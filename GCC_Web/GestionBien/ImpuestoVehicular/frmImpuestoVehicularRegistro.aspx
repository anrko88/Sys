<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImpuestoVehicularRegistro.aspx.vb"
    Inherits="GestionBien_ImpuestoVehicular_frmImpuestoVehicularRegistro" %>

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

    <script type='text/javascript' src="frmImpuestoVehicularRegistro.aspx.js"> </script>

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
                    <img alt="" src="../../Util/images/ico_mdl_impuesto.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Impuesto Vehicular :: Registro</div>
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
                    <div id="dv_img_buscar" class="dv_img_boton">
                        <a href="javascript:fn_buscar(true);">
                            <img alt="" src="../../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_img_limpiar" class="dv_img_boton">
                        <a href="javascript:fn_limpiar();">
                            <img src="../../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_lote" class="dv_img_boton" style="width: 80px;">
                        <a href="javascript:fn_AsignarLote()">
                            <img alt="" src="../../Util/images/ico_acc_lote.gif" border="0" /><br />
                            Generar Lote </a>
                    </div>
                    <div id="dv_img_anul" class="dv_img_boton" style="width: 80px;">
                        <a href="javascript:fn_AnularLote()">
                            <img alt="" src="../../Util/images/ico_acc_eliminar.gif" border="0" /><br />
                            Anular Lote </a>
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
                        <input id="hddRowIdImpuesto" type="hidden" runat="server" />
                        <input id="hddPlaca" type="hidden" runat="server" />
                        <input id="hddCodImpuesto" type="hidden" runat="server" />
                        <input id="hddNroCuotas" type="hidden" runat="server" />
                        <input id="hddSecFinanciamiento" type="hidden" runat="server" />
                        <input id="hddPeriodos" type="hidden" runat="server" />
                        <input id="hddFechaTransferencia" type="hidden" runat="server" />
                        <input id="hddCodigosImpuesto" type="hidden" runat="server" />
                        <input id="hddCheque" type="hidden" runat="server" />
                        <input id="hddNroLote" type="hidden" runat="server" />
                        <input id="hddPeriodo" type="hidden" runat="server" />
                        <input type="hidden" name="hddPerfil" id="hddPerfil" value="" runat="server" /><%--JJM IBK--%>
                        <%--Inicio IBK - AAE - Variable para ver si tengo el nro del lote asignado --%>
                        <input id="hidTengoLote" type="hidden" runat="server" />
                        <%--Fin IBK - AAE--%>
                        <input id="btnCargarImpuesto" type="button" value="button" runat="server" style="display: none"
                            onclick="javascript:fn_ListarImpuestosLote();" />
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
                                <%--Inicio IBK - AAE - Variable para ver si tengo el nro del lote asignado --%>
                                <tr>
                                    <td class="label">
                                        Nro Lote
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtNroLoteCarga" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Monto Cheque
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtMontoCheque" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Total Lote
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtTotalLote" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Devuelto
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtDevuelto" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        A Reembolsar
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtReembolsar" class="css_input" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Estado Lote
                                    </td>
                                    <td class="input">
                                        <input id="hidCodEstadoLote" type="hidden" runat="server" />
                                        <input type="text" id="txtEstadoLote" class="css_input" runat="server" />
                                    </td>                                
                                    <%--Fin IBK - AAE--%>
                                    <td class="label">
                                        Placa Actual
                                    </td>
                                    <td class="input">
                                        <input id="txtPlaca" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Nro Motor
                                    </td>
                                    <td class="input">
                                        <input id="txtMotor" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtCU" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtContrato" type="text" class="css_input" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Listado de Bienes
                                </td>
                            </tr>
                        </table>
                        <!-- Inicio Grilla Listado -->
                        <div class="dv_tabla_contenedoraSola">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td align="left">
                                        <%-- <div class="dv_img_boton_mini" id="dv_agregarImpuesto" style="border: 0">
                                            <a href="javascript:fn_abreNuevoImpuesto();" style="display: inline;">
                                                <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />Agregar Impuesto</a>
                                        </div>--%>
                                    </td>
                                </tr>
                            </table>
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
                                    Listado de Impuestos
                                </td>
                            </tr>
                        </table>
                        <!-- Inicio Grilla Listado -->
                        <div class="dv_tabla_contenedoraSola">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td align="left">
                                        <div class="dv_img_boton_mini" style="border: 0" id="dv_eliminar_impuesto" runat="server">
                                            <a href="javascript:fn_abreEliminarImpuesto();">
                                                <img alt="" src="../../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />Eliminar </a>
                                        </div>
                                        <div class="dv_img_boton_mini" id="dv_editar" style="border: 0">
                                            <a href="javascript:fn_abreEditarImpuesto();">
                                                <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />Editar </a>
                                        </div>
                                        <div id="dv_ver" class="dv_img_boton_mini" style="width: 100px; border: 0px solid #ffffff;"
                                            runat="server">
                                            <a href="javascript:fn_verImpuesto();">
                                                <img alt="" src="../../Util/images/ico_mdl_cotizacion.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Ver Impuesto</a>
                                        </div>
                                        <div class="dv_img_boton_mini" id="dv_agregarImpuesto" style="border: 0">
                                            <a href="javascript:fn_abreNuevoImpuesto();" style="display: inline;">
                                                <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />Agregar Impuesto</a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table id="jqGrid_lista_C">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_C">
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
