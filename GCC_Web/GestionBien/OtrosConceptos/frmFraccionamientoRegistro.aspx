<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFraccionamientoRegistro.aspx.vb"
    Inherits="OtrosConceptos_frmFraccionamientoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

    <script type='text/javascript' src="frmFraccionamientoRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmObservacion" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <img src="../../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo" id="divTitulo" runat="server">
                        Otros Conceptos
                    </div>
                    <div class="css_lbl_titulo" id="divSubTitulo" runat="server">
                        Cobros:: Fraccionamiento</div>
                </td>
                <td class="espacio">
                    &nbsp;
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
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            padding-right: 0px;">
            <tr>
                <td class="lineas">
                </td>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_Contrato" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 30px" valign="bottom">
                                    Datos del Fraccionamiento
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" width="100%">
                                <tr>
                                    <td class="label">
                                        Nro Cuotas
                                    </td>
                                    <td class="input" valign="bottom">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <input id="txtNroCuotas" name="txtNroCuotas" type="text" class="css_input" size="5"
                                                        runat="server" style="text-align: right" />
                                                </td>
                                                <td align="left" valign="bottom">
                                                    <div id="divGenerar" class="dv_img_boton_mini" style="width: 60px">
                                                        <a href="javascript:fn_Generar();">
                                                            <img src="../../Util/images/ico_acc_generar.jpg" border="0" />
                                                            Generar </a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                     <input id="txtMoneda" name="txtMoneda" type="text" class="css_input_inactivo" size="22"
                                            disabled="disabled" runat="server"  />
                                    </td>
                                   
                                    <td class="label">
                                        Importe
                                    </td>
                                    <td class="input">
                                        <input id="txtImporte" name="txtImporte" type="text" class="css_input_inactivo" size="13"
                                            disabled="disabled" runat="server" style="text-align: right" />
                                    </td>
                                    <td class="input">
                                    </td> 
                                </tr>
                            </table>
                        </div>
                        <table id="tb_Cobro" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 30px" valign="bottom">
                                    Lista de Cuotas
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table id="tb_Cuotas" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                <tr>
                                    <td id="tdCuotas" style="text-align: center">
                                        <table id="jqGrid_lista_A">
                                            <tr>
                                                <td />
                                            </tr>
                                        </table>
                                        <%--                                        <div id="jqGrid_pager_A">
                                        </div>--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </tr>
        </table>
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
    <asp:HiddenField ID="hdnTitulo" runat="server" />
    <asp:HiddenField ID="hidNroCuotas" runat="server" />
    <asp:HiddenField ID="hidOpcion" runat="server" />
    <asp:HiddenField ID="hidCodSolicitudCredito" runat="server" />
    <asp:HiddenField ID="hidTipoRubroFinanciamiento" runat="server" />
    <asp:HiddenField ID="hidCodIfi" runat="server" />
    <asp:HiddenField ID="hidTipoRecuperacion" runat="server" />
    <asp:HiddenField ID="hidNumSecRecuperacion" runat="server" />
    <asp:HiddenField ID="hidNumSecRecupComi" runat="server" />
    <asp:HiddenField ID="hidCodComisionTipo" runat="server" />
    <asp:HiddenField ID="hidFechaCobro" runat="server" />
    <asp:HiddenField ID="hidCodMoneda" runat="server" />
    <asp:HiddenField ID="hidTea" runat="server" />
    <asp:HiddenField ID="hidDiaAnio" runat="server" />
    <asp:HiddenField ID="hidEstadoDefecto" runat="server" />
    <asp:HiddenField ID="hidTasaDefecto" runat="server" />
    <asp:HiddenField ID="hidIGV" runat="server" Value = "0" />    
    <asp:HiddenField ID="hidFechaActivacion" runat="server" Value="" />
    <asp:HiddenField ID="hidFechaVencmiento" runat="server" Value="" />    
    <asp:HiddenField ID="hidCantidadPendiente" runat="server" Value="0" />  
    </form>
</body>
</html>
