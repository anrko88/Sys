<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCobroMasivoRegistro.aspx.vb"
    Inherits="GestionBien_OtrosConceptos_frmCobroMasivoRegistro" %>

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
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_A.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_B.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_C.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_D.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_E.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_F.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmCobroMasivoRegistro.aspx.js"> </script>

</head>

<script language="javascript" type="text/javascript">
    document.onkeydown = function(evt) { return (evt ? evt.which : event.keyCode) != 13; }
    document.onkeypress = function(evt) { if ((evt ? evt.which : event.keyCode) == 39) { event.keyCode = 96; return event.keyCode; } }
</script>

<body onload='fn_CheckFirstVisit();'>
    <form id="frmCobroMasivoRegistro" runat="server">
    <input id="hddtcdiaCompra" name="txttcdiaCompra" type="hidden" runat="server" />
    <input id="hddtcdiaVenta" name="txttcdiaVenta" type="hidden" runat="server" />
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- **************************************************************************************** -->
        <!-- BOTONES DE CABEZERA-->
        <!-- **************************************************************************************** -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos" style="width: 400px;">
                    <div class="css_lbl_subTitulo" id="lbl_SubTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo" id="lbl_titulo">
                        Otros Conceptos :: Registro de Cobros</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <%--                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscar(true);">
                            <img alt="" src="../../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="Div1" class="dv_img_boton">
                        <a href="javascript:fn_limpiar();">
                            <img src="../../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>                    
                    <div id="dv_separador" class="dv_img_boton_separador">
        :
    </div>--%>
                    <div class="dv_img_boton" style="width: 82px;">
                        <a href="javascript:fn_enviarCarta();">
                            <img alt="" src="../../Util/images/ico_acc_msgEnviar.gif" border="0" /><br />
                            Enviar Carta </a>
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
                        <table id="tb_Comprobantes" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                    Lista de Cobros
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td colspan="6" style="text-align: right; padding-right: 10px; height: 30px" valign="bottom">
                                        <div id="dv_eliminar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
                                            dir="ltr">
                                            <a href="javascript:fn_EliminarConcepto();">
                                                <img alt="" src="../../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Eliminar </a>
                                        </div>
                                        <div id="dv_editar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;">
                                            <a href="javascript:fn_EditarConcepto('0');">
                                                <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Editar </a>
                                        </div>
                                        <div id="dv_agregar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;">
                                            <a href="javascript:fn_AgregarConcepto();">
                                                <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Agregar </a>
                                        </div>
                                        <div id="dv_Franccionar" class="dv_img_boton_mini" style="width: 80px; border: 0px solid #ffffff;">
                                            <a href="javascript:fn_FraccionarConcepto();">
                                                <img alt="" src="../../Util/images/ico_acc_replicar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Fraccionar </a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hidtotalDocs" runat="server" Value="0" />
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                <tr>
                                    <td id="tbCronograma" style="text-align: center">
                                        <table id="jqGrid_lista_A">
                                            <tr>
                                                <td />
                                            </tr>
                                        </table>
                                        <div id="jqGrid_pager_A">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div style="text-align: right;" id="divTotal">
                            </div>
                        </div>
                    </td>
                </tr>
            </table>            
            <asp:HiddenField ID="hidOpcion" runat="server" />
            <asp:HiddenField ID="hidCodSolicitudCredito" runat="server" />
            <asp:HiddenField ID="hidTipoRubroFinanciamiento" runat="server" />
            <asp:HiddenField ID="hidCodIfi" runat="server" />
            <asp:HiddenField ID="hidTipoRecuperacion" runat="server" />
            <asp:HiddenField ID="hidNumSecRecuperacion" runat="server" />
            <asp:HiddenField ID="hidNumSecRecupComi" runat="server" />
            <asp:HiddenField ID="hidCodComisionTipo" runat="server" />
            <asp:HiddenField ID="hidEstadoRecuperacion" runat="server" />
            <asp:HiddenField ID="hidFlagIndividual" runat="server" />
            <asp:HiddenField ID="hidFlagRegistro" runat="server" />
            <asp:HiddenField ID="hidItem" runat="server" />
            <asp:HiddenField ID="hidInstancia" runat="server" />
            <asp:HiddenField ID="hidRefresh" runat="server" Value="0" />
            <asp:HiddenField ID="hidFechaCobro" runat="server" />
            <asp:HiddenField ID="hidCodMoneda" runat="server" />
            <asp:HiddenField ID="hidTea" runat="server" />
            <asp:HiddenField ID="hidDiaAnio" runat="server" />            
            <asp:HiddenField ID="hidTasaDefecto" runat="server" />
            <asp:HiddenField ID="hidIGV" runat="server" Value="0" />
            <asp:HiddenField ID="hidFechaVencmiento" runat="server" Value="" />
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
    </form>
</body>
</html>
