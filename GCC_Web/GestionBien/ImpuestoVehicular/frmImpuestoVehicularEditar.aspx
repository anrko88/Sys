<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImpuestoVehicularEditar.aspx.vb" Inherits="GestionBien_ImpuestoVehicular_frmImpuestoVehicularEditar" %>

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

    <script type='text/javascript' src="frmImpuestoVehicularEditar.aspx.js"> </script>

</head>
<body>
    <form id="frmImpuestoVehicularEditar" runat="server">
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
                        Impuesto Vehicular :: Detalle</div>
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
                   
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Grabar()">
                            <img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
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
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contrato y del Bien
                                </td>
                                <%--<td class="botones">
                                    <img src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>--%>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                    <td class="label">
                                       CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtCUCliente" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Estado del Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoContrato" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                    <td class="label">
                                       Clasificación del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtClasificacionBien" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                    <td class="label">
                                        Tipo de Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtTipoBien" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <input id="txtMoneda" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                    <td class="label">
                                       Valor del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtValor" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                    <td class="label">
                                        Placa
                                    </td>
                                    <td class="input">
                                        <input id="txtPlaca" type="text" class="css_input_inactivo" disabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Impuesto
                                </td>
                                <%--<td class="botones">
                                    <img src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>--%>
                            </tr>
                        </table>
                   
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                <td class="label">
                                    Fecha Declaración
                                </td>
                                <td class="input">
                                    <input id="txtFechaDeclaracion" name="txtFechaDeclaracion" type="text" class="css_input"  runat="server"  />
                                </td>
                                <td class="label">
                                    Periodo
                                </td>
                                <td class="input">
                                    <input id="txtPeriodo" name="txtPeriodo" type="text" class="css_input"  runat="server"  />
                                </td>
                                <td class="label">
                                    Importe
                                </td>
                                <td class="input">
                                    <input id="txtImporte" name="txtImporte" type="text" class="css_input"  runat="server"  />
                                </td>  
                            </tr>
                            <tr>
                                <td class="label">
                                    Nº Cuota
                                </td>
                                <td class="input">
                                    <select id="ddlCuota" runat="server">
                                        <option value="0">- Seleccionar -</option>
                                    </select>
                                </td>
                                <td class="label">
                                    Pago Cliente
                                </td>
                                <td class="input">
                                    <input id="cbPagoCliente" type="checkbox"  />
                                </td>
                                <td class="label">
                                    Fecha Pago
                                </td>
                                <td class="input">
                                    <input id="txtFechaPago" name="txtFechaPago" type="text" class="css_input"  runat="server"  />
                                </td>  
                            </tr>
                            <tr>
                                <td class="label">
                                    Estado Pago
                                </td>
                                <td class="input">
                                     <select id="ddlEstadoPago" runat="server">
                                        <option value="0">- Seleccionar -</option>
                                    </select>
                                </td>
                                <td class="label">
                                    Fecha Cobro
                                </td>
                                <td class="input">
                                    <input id="txtFechaCobro" name="txtFechaCobro" type="text" class="css_input"  runat="server"  />
                                </td>
                                <td class="label">
                                    Estado Cobro
                                </td>
                                <td class="input">
                                    <select id="ddlEstadoCobro" runat="server">
                                        <option value="0">- Seleccionar -</option>
                                    </select>
                                </td>  
                            </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
