<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMultaVehicularListado.aspx.vb"
    Inherits="GestionBien_MultaVehicular_frmMultaVehicularListado" %>

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

    <script type='text/javascript' src="frmMultaVehicularListado.aspx.js"> </script>

</head>
<body>
    <form id="frmDemandaListado" runat="server">
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
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Multa Vehicular :: Listado</div>
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
                            <img alt="" src="../../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreEditar('')">
                            <img alt="" src="../../Util/images/ico_acc_editar.gif" border="0" /><br />
                            <!-- Inicio IBK - AAE - Se cambia Label -->
                            Editar Lote</a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width: 60px;">
                        <a href="javascript:fn_abreNuevo();">
                            <img alt="" src="../../Util/images/ico_acc_agregar.gif" border="0" /><br />
                            Agregar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width: 60px;">
                        <a href="javascript:fn_abreAsignarCheque();">
                            <img alt="" src="../../Util/images/ico_acc_cheque.gif" border="0" /><br />
                            Asignar Cheque </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width: 60px;">
                        <a href="javascript:fn_abreLiquidacion();">
                            <img alt="" src="../../Util/images/ico_acc_liquidar.gif" border="0" /><br />
                            Liquidar </a>
                    </div>
                </td>
            </tr>
        </table>
        <input id="btnBuscarMultas" type="hidden" runat="server" />
        <input id="btnCargarMulta" type="button" value="button" runat="server" style="display: none"
            onclick="javascript:fn_ListarMulta();" />
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <input id="hddRowId" type="hidden" />
                        <input id="hddDistrito" type="hidden" />
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos de Búsqueda
                                </td>
                                <td class="botones">
                                    <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
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
                                        <input id="txtNroContrato" type="text" class="css_input" />
                                    </td>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtcuCliente" type="text" class="css_input" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocialNombre" type="text" class="css_input" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo de documento
                                    </td>
                                    <td class="input">
                                        <select id="ddlTipoDocumento" runat="server" name="ddlTipoDocumento">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Nº Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocumento" type="text" class="css_input" />
                                    </td>
                                    <td class="label">
                                        Tipo de Bien
                                    </td>
                                    <td class="input">
                                        <select id="ddlTipoBien" runat="server" name="ddlTipoBien">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Placa Actual
                                    </td>
                                    <td class="input">
                                        <input id="txtPlaca" type="text" class="css_input" />
                                    </td>
                                    <td class="label">
                                        Nº Lote
                                    </td>
                                    <td class="input">
                                        <input id="txtNroLote" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Municipalidad
                                    </td>
                                    <td class="input">
                                        <input id="txtCodMunicipalidad" runat="server" class="css_input" size="5" />
                                        <input id="txtMunicipalidad" runat="server" class="css_input" size="50" />
                                        <img id="imgBsqMunicipalidad" alt="" src="../../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                            vertical-align: top;" runat="server" />
                                        <%--<select id="ddlMunicipalidad" runat="server" name="ddlMunicipalidad">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Concepto
                                    </td>
                                    <td class="input">
                                        <select id="ddlConcepto" runat="server" name="ddlConcepto">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Código Infracción
                                    </td>
                                    <td class="input">
                                        <select id="ddlCodInfraccion" runat="server" name="ddlCodInfraccion">
                                            <option value='0'>---</option>
                                        </select>
                                        -
                                        <input id="txtCodigoInfraccion" type="text" class="css_input" style="width: 45px" />
                                    </td>
                                    <td class="label">
                                        Estado Pago
                                    </td>
                                    <td class="input">
                                        <select id="ddlEstadoPago" runat="server" name="ddlEstadoPago">
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
