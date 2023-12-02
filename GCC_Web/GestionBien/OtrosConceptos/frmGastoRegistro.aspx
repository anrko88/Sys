<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGastoRegistro.aspx.vb"
    Inherits="GestionBien_OtrosConceptos_frmGastoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    
    
    <!-- Estilos --> 
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css" media="all" />
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
    <script type='text/javascript' src="frmGastoRegistro.aspx.js"> </script>

</head>

<script language="javascript" type="text/javascript">
    document.onkeydown = function(evt) { return (evt ? evt.which : event.keyCode) != 13; }
    document.onkeypress = function(evt) { if ((evt ? evt.which : event.keyCode) == 39) { event.keyCode = 96; return event.keyCode; } }
</script>

<body>
    <form id="frmGastoRegistro" runat="server">
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
                        Otros Conceptos(Gastos) :: Registro de Documentos</div>
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
                    <!--
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_grabar();">
                            <img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
                    </div>
                    
                    <div id="Div2" class="dv_img_boton" style="width: 82px;">
                        <a href="javascript:fn_enviarCarta();">
                            <img alt="" src="../../Util/images/ico_acc_msgEnviar.gif" border="0" /><br />
                            Enviar Carta </a>
                    </div>
                    -->
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
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Documento
                                </td>
                                <td class="botones">
                                    <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table border="0" cellpadding="0" cellspacing="3" width="100%" id="tb_formulario">
                                <tr>
                                    <td class="label">
                                        Tipo de Documento
                                    </td>
                                    <td class="input">
                                        <select id="cmdTipoDoc" name="cmdTipoDoc" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">
                                        Nº Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocProveed" name="txtNroDocProveed" type="text" class="css_input"
                                            size="18" onblur='javascript:fn_buscarProveedor();' runat="server" />
                                        <img id="imgBuscarProveedor" alt="" src="../../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                            vertical-align: middle;" onclick="javascript:VentanaProveedores();" />
                                        <asp:HiddenField ID="hidCodProveedor" runat="server" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocialProveedor" name="txtRazonSocialProveedor" type="text" class="css_input_inactivo"
                                            size="45" disabled="disabled" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo de Comprobante
                                    </td>
                                    <td class="input">
                                        <input id="txtNumeroTipo" name="txtNumeroTipo" type="text" class="css_input" runat="server"
                                            size="4" value="" />
                                        <img alt="" src="../../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                            id="imgNumeroTipo" runat="server" />
                                        <asp:Label ID="lblNumeroTipo" runat="server" class="label_mini"></asp:Label>
                                    </td>
                                    <td class="label">
                                        Nº Comprobante
                                    </td>
                                    <td class="input">
                                        <input id="txtSerieDoc1" name="txtSerieDoc1" type="text" class="css_input" size="5"
                                            value="" />
                                        &nbsp;-&nbsp;
                                        <input id="txtNumeroDoc1" name="txtNumeroDoc1" runat="server" type="text" class="css_input"
                                            size="20" value="" />
                                    </td>
                                    <td class="label">
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <select id="cmbMoneda" name="cmbMoneda" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">
                                        F. Emisión
                                    </td>
                                    <td class="input">
                                        <!-- Inicio IBK -->
                                        <!--<input id="txtFechaEmision1" name="txtFechaEmision" type="text" class="css_input"
                                                size="12" runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />-->
                                        <input id="txtFechaEmision" name="txtFechaEmision" type="text" class="css_input"
                                            onblur="fn_valFecha('1');" size="12" runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />
                                        <!-- Fin IBK -->
                                    </td>
                                    <td class="label">
                                    </td>
                                    <td class="input">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Gravado
                                    </td>
                                    <td class="input">
                                        <input id="txtGravado" name="txtGravado" type="text" class="css_input" size="10"
                                            runat="server" style="text-align: right" />
                                    </td>
                                    <td class="label">
                                        IGV
                                    </td>
                                    <td class="input">
                                        <input id="txtPorcIGV" name="txtIGV" type="text" class="css_input" size="2" runat="server" />
                                        &nbsp;%-&nbsp;
                                        <input id="txtNumeroIGV" name="txtNumeroIGV" type="text" class="css_input" size="15"
                                            runat="server" style="text-align: right" />
                                    </td>
                                    <td class="label">
                                        No Gravado
                                    </td>
                                    <td class="input">
                                        <input id="txtValorNoGravado" name="txtValorNoGravado" type="text" class="css_input"
                                            size="10" runat="server" style="text-align: right" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Total
                                    </td>
                                    <td class="input">
                                        <input id="txtTotal" name="txtTotal" type="text" class="css_input_inactivo" size="10"
                                            runat="server" disabled="disabled" style="text-align: right" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="label">
                                        <span id="spn_4taLabel">Renta 4ta Categ. </span>
                                    </td>
                                    <td class="input">
                                        <span id="spn_4taInput">
                                            <input id="txtPorc4ta" name="txtPorc4ta" type="text" class="css_input" size="2" runat="server" />
                                            %&nbsp;-&nbsp;&nbsp;S/.&nbsp;
                                            <input id="txtMonto4taSoles" name="txtMonto4taSoles" type="text" class="css_input_inactivo"
                                                size="10" runat="server" disabled="disabled" style="text-align: right" />
                                            &nbsp;$&nbsp;
                                            <input id="txtMonto4taDolares" name="txtMonto4taDolares" type="text" class="css_input_inactivo"
                                                size="10" runat="server" disabled="disabled" style="text-align: right" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        T.C. del Día
                                    </td>
                                    <td class="input">
                                        <input id="hddtcdiaVenta" name="txttcdiaVenta" type="hidden" runat="server" />
                                        <input id="hddtcdiaCompra" name="txttcdiaCompra" type="hidden" runat="server" />
                                        <input id="txttcdia" name="txttcdia" type="text" class="css_input" size="10" runat="server"
                                            style="text-align: right" />
                                    </td>
                                    <td class="label">
                                        <input id="chktcespecial" name="chktcespecial" type="checkbox" runat="server" onclick="TCEspecial()" />
                                        T.C. Especial
                                    </td>
                                    <td class="input">
                                        <input id="txttcespecial" name="txttcespecial" type="text" class="css_input_inactivo"
                                            size="7" value="" runat="server" style="text-align: right" />
                                    </td>
                                    <td class="label">
                                        <input id="chkSunat" name="chkSunat" type="checkbox" style="display: none" runat="server" />T.
                                        C. Sunat
                                    </td>
                                    <td class="input">
                                        <input id="txtTipoCambioSunat" name="txtTipoCambioSunat" type="text" class="css_input_inactivo"
                                            size="10" readonly="readonly" disabled="disabled" style="text-align: right" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" colspan="2" rowspan="2" style="text-align: center">
                                        <table style="width: 80%">
                                            <tr>
                                                <td>
                                                    <input id="chkNinguno" name="chkNinguno" type="radio" runat="server" />
                                                    Ninguno
                                                </td>
                                                <td>
                                                    <input id="chkDetraccion" name="chkNinguno" type="radio" runat="server" />
                                                    Detracción
                                                </td>
                                                <td>
                                                    <input id="ChkRetencion" name="chkNinguno" type="radio" runat="server" />
                                                    Retención
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hidAgenteRetencion" runat="server" Value="0" />
                                    </td>
                                    <td class="label">
                                        Tipo
                                    </td>
                                    <td class="input">
                                        <input id="txtTipoBien" name="txtTipoBien" type="text" class="css_input" runat="server"
                                            size="4" value="" />
                                        <img alt="" src="../../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                            id="imgTipoBien" runat="server" />
                                        <asp:Label ID="lblTipoBien" runat="server" class="label"></asp:Label>
                                        <asp:HiddenField ID="hidtxtTipoBien" runat="server" />
                                        <asp:HiddenField ID="hidValorCompara" runat="server" />
                                    </td>
                                    <td class="label">
                                        Montos S/.
                                    </td>
                                    <td class="input">
                                        <input id="txtMontoSoles" name="txtserieDocumento" type="text" class="css_input_inactivo"
                                            size="10" value="" runat="server" disabled="disabled" style="text-align: right" />
                                        &nbsp;$&nbsp;
                                        <input id="txtMontoDolar" name="txtNumeroDocumento" type="text" class="css_input_inactivo"
                                            size="10" value="" runat="server" disabled="disabled" style="text-align: right" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Nº Constancia
                                    </td>
                                    <td class="input">
                                        <input id="txtNroConstancia" name="txtNroConstancia" type="text" class="css_input"
                                            size="30" style="text-align: right" value="" runat="server" />
                                    </td>
                                    <td class="label">
                                        F. Emisión
                                    </td>
                                    <td class="input">
                                        <!-- Inicio IBK -->
                                        <input id="txtFechaCompro" name="txtFechaEmision" type="text" class="css_input" size="12"
                                            onblur="fn_valFecha('3');" runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />
                                        <!-- Fin IBK -->
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tb_Comprobantes" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 30px" valign="bottom">
                                    Documentos
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td colspan="6" style="text-align: right; padding-right: 10px; height: 30px" valign="bottom">
                                        <div id="dv_cancelar" class="dv_img_boton_mini" style="width: 80px; border: 0px solid #ffffff;"
                                            runat="server">
                                            <a href="javascript:fn_CancelarDocumento();">
                                                <img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Cancelar </a>
                                        </div>
                                        <div id="dv_Modificar" class="dv_img_boton_mini" style="width: 80px; border: 0px solid #ffffff;"
                                            runat="server">
                                            <a href="javascript:fn_ModificarDocumento();">
                                                <img alt="" src="../../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Modificar </a>
                                        </div>
                                        <div id="dv_eliminar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
                                            dir="ltr">
                                            <a href="javascript:fn_EliminarDocumento();">
                                                <img alt="" src="../../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Anular </a>
                                        </div>
                                        <div id="dv_editar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;">
                                            <a href="javascript:fn_EditarDocumento();">
                                                <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Editar </a>
                                        </div>
                                        <div id="dv_agregar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;">
                                            <a href="javascript:fn_AgregarDocumento();">
                                                <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Agregar </a>
                                        </div>
                                        <div id="dv_EditarReplicar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;">
                                            <a href="javascript:fn_ReplicarDocumento();">
                                                <img alt="" src="../../Util/images/ico_acc_replicar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Replicar </a>
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
        </div>
    </div>    
    
    <asp:HiddenField ID="hidTipoDocumento" runat="server" />
    <asp:HiddenField ID="hidFecEmision" runat="server" />  
    <asp:HiddenField ID="hidCodigoEstadoDoc" runat="server" />
    <asp:HiddenField ID="hidTipoCambioDia" runat="server" />
    <asp:HiddenField ID="hidTipoCambioSunat" runat="server" />
    <asp:HiddenField ID="hidCodMoneda" runat="server" />
    <input type="hidden" id="hddFechaActual" name="hddFechaActual" value="" runat="server" />
    <asp:HiddenField ID="hidTipoComprobante" runat="server" />
    </form>
</body>
</html>
