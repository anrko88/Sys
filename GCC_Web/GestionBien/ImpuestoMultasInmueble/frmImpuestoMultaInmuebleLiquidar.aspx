<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImpuestoMultaInmuebleLiquidar.aspx.vb"
    Inherits="GestionBien_ImpuestoMultasInmueble_frmImpuestoMultaInmuebleLiquidar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documentos y Comentarios</title>
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

    <script type='text/javascript' src="frmImpuestoMultaInmuebleLiquidar.aspx.js"></script>

</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_impuesto.gif" />
                </td>
                <td class="titulos" style="width: 350px;">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Impuestos y Multas Inmuebles :: Liquidar</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Buscar(true);">
                            <img alt="" src="../../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Reporte();">
                            <img alt="" src="../../Util/images/ico_acc_importar.gif" border="0" /><br />
                            Exportar </a>
                    </div>
                      <%--Inicio JJM IBK--%>
                    <div id="dv_img_botonCobraLote" class="dv_img_boton">
                        <a href="javascript:fn_CobrarLote()">
                            <img alt="" src="../../Util/images/ico_acc_liqLote.gif" border="0" /><br />
                            Liquidar Lote </a>
                    </div>
                    <%-- Fin JJM IBK--%>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                padding-right: 0px;">
                <tr>
                    <td class="lineas">
                        <asp:Button ID="btnGenerar" runat="server" Style="display: none" />
                        <asp:HiddenField ID="hidCodigos" runat="server" Value="" />
                        <asp:HiddenField ID="hidTotal" runat="server" Value="0" />
                         <input id="hidNroLote" type="hidden" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <input type="hidden" name="hddTipoTx" id="hddTipoTx" value="" runat="server" />
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nº Lote
                                    </td>
                                    <td class="input">
                                        <input id="txtNroLote" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Nº Cheque
                                    </td>
                                    <td class="input">
                                        <input id="txtNroCheque" type="text" class="css_input_inactivo" runat="server" />
                                    </td>
                                    <%--Inicio JJM IBK--%>
                                     <td class="label">
                                       Estado 
                                    </td>
                                    <td class="input">
                                        <input id="hidCodEstadoLote" type="hidden" runat="server" />
                                        <input id="txtDescEstadoLote" type="text" class="css_input_inactivo" runat="server" />
                                    </td>
                                    <%-- Fin JJM IBK--%>
                                  
                                    <%-- Inicio IBK AAE--%>
                                    <td class="label">
                                        Fecha Cobro
                                    </td>
                                    <td class="input">
                                        <input type="text"  id="txtFechaCobro" runat="server" class="css_input_inactivo" />
                                    </td>
                                    <td class="label">
                                        Fecha Pago
                                    </td>
                                    <td class="input">
                                        <input type="text"  id="txtFechaPago" runat="server" class="css_input_inactivo" />
                                    </td>
                                    <%-- Fin  IBK--%>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Concepto
                                    </td>
                                    <td>
                                        <input  id="txtDescConcepto" type="text" class="css_input_inactivo" runat="server" />
                                        <input id="hidConcepto" type="hidden" runat="server" />
                                    </td>
                                     <td class="label">
                                        Monto Cheque
                                    </td>
                                    <td class="input">
                                        <input type="text"  id="txtMontoCheque" runat="server" class="css_input_inactivo" />
                                    </td>
                                    <td class="label">
                                        Monto Devuelto
                                    </td>
                                    <td class="input">
                                        <input type="text"  id="txtMontoDevuelto" runat="server" class="css_input_inactivo" />
                                    </td>
                                    <td class="label">
                                        Monto Reembolsar
                                    </td>
                                     <td class="input">
                                        <input type="text"  id="txtMontoReembolsar" runat="server" class="css_input_inactivo" />
                                    </td>
                                      <td class="label">
                                        Total
                                    </td>
                                    <td class="input">
                                        <input type="text"  id="txtTotal" runat="server" class="css_input_inactivo" />
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
