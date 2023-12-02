<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitudDocumentoProveedorRegistro.aspx.vb"
    Inherits="Verificacion_frmSolicitudDocumentoProveedorRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css"
        media="all" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_fuente.css" />
    <!-- JavaScript -->
	<script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"> </script>
	<script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"> </script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.js"> </script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"> </script>		
	<script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"> </script>
	
	<script type='text/javascript' src="../Util/js/jquery/jquery.ui.global.js"> </script>	
	<script type='text/javascript' src="../Util/js/jquery/jquery.validText.js"> </script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.validNumber.js"> </script>		
	<script type='text/javascript' src="../Util/js/jquery/jquery.maxlength.js"> </script>		
	<script type="text/javascript" src="../Util/js/jquery/json2.js" ></script>
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>
	
    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    
    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
    <!--
    <script src="../Util/js/jquery/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>
     -->
    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>
   

    <!-- JQGrid -->
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmSolicitudDocumentoProveedorRegistro.aspx.js"> </script>

</head>

<script language="javascript" type="text/javascript">
    document.onkeydown = function(evt) { return (evt ? evt.which : event.keyCode) != 13; }
    document.onkeypress = function(evt) { if ((evt ? evt.which : event.keyCode) == 39) { event.keyCode = 96; return event.keyCode; } }
</script>

<body>
    <form id="frmCotizacionSolicitudDocumentoProveedor" runat="server">
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
                    <img alt="" src="../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo" id="lbl_SubTitulo">
                        Verificación :: Solicitud de Documentos</div>
                    <div class="css_lbl_titulo" id="lbl_titulo">
                        Proveedor :: Editar</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <!--
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_grabar();">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
                    </div>
                    -->
                    <div id="Div2" class="dv_img_boton" style="width: 82px;">
                        <a href="javascript:fn_enviarCarta();">
                            <img alt="" src="../Util/images/ico_acc_msgEnviar.gif" border="0" /><br />
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
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos de la Cotización
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        
                        <!-- Inicia de Cabecera !-->
        			    <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" value="" runat="server" />
	                    <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" value="" runat="server" />
	                    <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" value="" runat="server" />
	                    <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" value="" runat="server" />
	                    <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" value="" runat="server" />
        			    <input type="hidden" id="hidListaDocumento" name="hidListaDocumento" value="" runat="server" />
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                <colgroup>
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                </colgroup>
                                <tr>
                                    <td class="label">
                                        Nº Cotización
                                    </td>
                                    <td class="input">
                                        <input id="txtNroCotizacion" name="txtNroCotizacion" type="text" class="css_input_inactivo"
                                            value="" size="15" readonly="readonly"  disabled="disabled" runat="server" />
                                    </td>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtCUCliente" name="txtCUCliente" class="css_input_inactivo" size="15"
                                            readonly="readonly" disabled="disabled" runat="server" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" name="txtRazonSocial" type="text" class="css_input_inactivo"
                                            value="" size="30" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNumContrato" type="text" class="css_input_inactivo" value="" size="15"
                                            readonly="readonly" disabled="disabled" runat="server" />
                                    </td>
                                    <td class="label">
                                        Tipo Persona
                                    </td>
                                    <td class="input">
                                        <input id="txtTipoPersona" name="txtTipoPersona" type="text" class="css_input_inactivo"
                                            value="" size="15" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    <td class="label">
                                        Procedencia
                                    </td>
                                    <td class="input">
                                        <input id="txtProcedencia" name="txtProcedencia" type="text" class="css_input_inactivo"
                                            value="" size="15" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    
                                </tr>
                                
                                <tr>
                                    <td class="label">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtClasificacionBien" name="txtClasificacionBien" type="text" class="css_input_inactivo"
                                            value="" size="40" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    <td class="label">
                                        Tipo de Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtTipoInmueble" name="txtTipoInmueble" type="text" class="css_input_inactivo"
                                            value="" size="15" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    
                                    <td class="label">
                                       Moneda 
                                    </td>
                                    <td class="input">
                                        <input id="txtMonedaContrato" disabled="disabled" name="txtMonedaContrato" type="text" class="css_input_inactivo" value="" size="15" readonly="readonly" runat="server" />
                                    </td>
                                    
                                    
                                    
                                </tr>
                                <tr>
                                    <td class="label">
                                        Valor Venta
                                    </td>
                                    <td class="input">
                                        <input id="txtValorVenta" name="txtValorVenta" type="text" class="css_input_inactivo"
                                            value="" size="15" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    <td class="label">
                                        IGV
                                    </td>
                                    <td class="input">
                                        <input id="txtIgv" name="txtIgv" type="text" class="css_input_inactivo" value=""
                                            size="15" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    <td class="label">
                                        Precio Venta
                                    </td>
                                    <td class="input">
                                        <input id="txtPrecioVenta" name="txtPrecioVenta" type="text" class="css_input_inactivo"
                                            value="" size="15" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                  <td >
                                        
                                    </td>
                                    <td class="input">
                                        <input id="chkTerminoRecepcion" name="chkTerminoRecepcion" type="checkbox" runat="server" disabled="disabled"   />
                                        <input id="hddTerminoRecepcion" name="hddTerminoRecepcion" type="hidden" value="0" />
                                        <input id="hidMensajeCorreo" name="hidMensajeCorreo" type="hidden" value="" runat="server" />
                                        <input id="txtFechaTerminoRecepcion" type="text" class="css_input_inactivo" value="" size="15" disabled="disabled" runat="server" readonly="readonly" />
                                    </td>
                                    
                                </tr>
                            </table>
                        </div>
                        <br />
                        <table id="Table1" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos Carta
                                </td>
                            </tr>
                        </table>
                        <div id="Div1" class="dv_tabla_contenedora">
                            <asp:HiddenField ID="hidNumeroContrato" runat="server" Value="" />
                            <asp:HiddenField ID="hidMonedaContrato" runat="server" Value="" />
                            <asp:HiddenField ID="hidTipoCompra" runat="server" Value="0" />
                            <asp:HiddenField ID="hidMontoTipoCambio" runat="server" Value="0" />
                            <asp:HiddenField ID="hidTipoVenta" runat="server" Value="0" />
                            <asp:HiddenField ID="hidCodigoContratoProveedor" runat="server" Value="0" />
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                <colgroup>
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                    <col style="width: 16.66%" />
                                </colgroup>
                                <tr>
                                    <td class="label">
                                        Proveedor
                                    </td>
                                    <td class="input">
                                        <input type="hidden" id="hddCodProveedor" name="hddCodProveedor" />
                                        <input type="hidden" id="hidNumCuentaD" name="hidNumCuentaD" />
                                        <input type="hidden" id="hidNumCuentaS" name="hidNumCuentaS" />
                                        <input id="txtRucProveedor" name="txtRucProveedor" type="text" class="css_input"
                                            size="14" value="" runat="server" onblur='javascript:fn_buscarProveedor();' />
                                        <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                            onclick="javascript:fn_abreBusquedaProveedor();" />
                                    </td>
                                    <td class="label">
                                        Tipo Proveedor
                                    </td>
                                    <td class="input">
                                        <select id="cmbTipoProveedor" name="cmbTipoProveedor" runat="server" disabled="disabled" >
                                            <option value="0">- Seleccionar -</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Razón social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocialProveedor" name="txtRazonSocialProveedor" type="text"  class="css_input_inactivo"
                                            readonly="true" size="30" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Contacto
                                    </td>
                                    <td class="input">
                                        <asp:HiddenField ID="hidValueContacto" runat="server" Value="" />
                                        <asp:HiddenField ID="hidCodContacto" runat="server" Value="0" />
                                        <table border="0" cellpadding="0" cellspacing="0" id="tbCombo" style="width:250px;">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="cmbContacto" runat="Server" onchange='javascript:fn_selectContacto();'>
                                                        <asp:ListItem Value="0">- Seleccione -</asp:ListItem>
                                                    </asp:DropDownList>                                            
                                                    <asp:ImageButton ID="imbAgregar" ImageUrl="~/Util/images/ico_acc_agregar.gif" runat="server"
                                                        OnClientClick="return fn_agregarContacto();" Width="16" Height="16" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" id="tbTexto">
                                            <tr>
                                                <td>
                                                    <input id="txtContacto" name="txtContacto" type="text" class="css_input" size="20"
                                                        value="" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imbCancelar" ImageUrl="~/Util/images/ico_acc_cancelar.gif" Width="16"
                                                        Height="16" runat="server" OnClientClick="return fn_cancelarContacto();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="label">
                                        Correo
                                    </td>
                                    <td class="input">
                                        <input id="txtCorreoProveedor" name="txtCorreoProveedor" type="text" class="css_input"
                                            size="40" value="" runat="server" />
                                    </td>
                                </tr>
                                <tr id ="trdescripcionbien">
                                    <td class="label">
                                        Descripción del Bien
                                    </td>
                                    <td class="input" colspan="5">
                                        <textarea id="txadescripcionbien" name="txadescripcionbien" cols="95" rows="2" runat="server"></textarea>
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
                                        Importe a Facturar
                                    </td>
                                    <td class="input">
                                        <input id="txtImporte" name="txtImporte" type="text" class="css_input" size="20"
                                            value="" style="text-align: right;" />
                                            
                                    </td>
                                </tr>
                            </table>
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                <tr>
                                    <td colspan="6" align="left">
                                        <div id="dv_botonesEditar">
                                            <div class="dv_img_boton_mini" id="divCancelar" style="border: 0">
                                                    <a href="javascript:fn_cancelarProveedor();">
                                                    <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />&nbsp;&nbsp;&nbsp; Cancelar </a>
                                            </div>   
                                            <div class="dv_img_boton_mini" id="div3" style="border: 0">
                                                    <a href="javascript:fn_GuardaProveedor();">
                                                    <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />&nbsp;&nbsp;&nbsp; Guardar </a>
                                            </div>   
                                        </div>
                                        
                                        
                                        <div id="dv_botonesAgregar">                                                                         
                                            <div class="dv_img_boton_mini" id="divAgregar" style="border: 0">
                                                   <a href="javascript:fn_agregarProveedor();" style="display: inline;">
                                                    <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                    display: inline;" border="0" />Agregar </a>
                                             </div>
                                            <div class="dv_img_boton_mini" style="border: 0">
                                                    <a href="javascript:fn_eliminarProveedor();">
                                                    <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />Eliminar </a>
                                            </div>
                                            <div class="dv_img_boton_mini" id="divEditar" style="border: 0">
                                                    <a href="javascript:fn_editarProveedor();">
                                                    <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />&nbsp;&nbsp;&nbsp; Editar </a>
                                            </div>
                                        </div>                                                                         
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" colspan="6">
                                        <div class="dv_tabla_contenedora">
                                            <table id="jqGrid_lista_F">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div style="text-align: right;" id="divTotal">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="css_lbl_mini">
                                        <strong>Tipo Cambio:</strong> Compra=<%= hidTipoCompra.Value %>&nbsp;&nbsp;&nbsp;
                                        Venta=<%=hidTipoVenta.Value%>&nbsp;&nbsp;&nbsp;Modalidad T.C.=&nbsp;&nbsp;&nbsp;<%=hddModalidadTipoCambio.value%>&nbsp;&nbsp;&nbsp;
                                        <%--Fecha Tipo Cambio=&nbsp;<%=hddFechaSolicitudCredito.Value%>--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <input id="hddPagChecked" name="hddPagChecked" type="hidden" />
                <input id="hddtotal" name="hddtotal" type="hidden"/>
                
                <input id="hddFechaSolicitudCredito" name="hddFechaSolicitudCredito" type="hidden" runat="server"/>
                <input id="hddModalidadTipoCambio" name="hddModalidadTipoCambio" type="hidden" runat="server"/>
                <input id="hddFlagMensajeTotal" name="hddFlagMensajeTotal" type="hidden" runat="server"/>
                <input id="hddTotalDocumentoProveedor" name="hddTotalDocumentoProveedor" type="hidden" runat="server"/>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
