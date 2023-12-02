<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDesembolsoRegistro.aspx.vb"
    Inherits="Desembolso_frmDesembolsoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    
    <!-- Icono URL --> 
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
    
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css" media="all" />
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
    <script type="text/javascript" src="../Util/js/jquery/json2.js"></script>
    <script type="text/javascript" src="../Util/js/js_global.js"></script>
    <script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
    <script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>
    <script type='text/javascript' src="../Util/js/js_util.date.js"> </script>
    <script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>
    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>

    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>
    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->
    <script type='text/javascript' src="frmDesembolsoRegistro.aspx.js"></script>

</head>
<body>
    <form runat="server" id="frmDesembolsoRegistro">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Desembolso</div>
                    <div class="css_lbl_titulo">
                        Registro Documentos :: Editar</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton" style="width: 60px">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width: 60px; display: none;">
                        <a href="javascript:fn_grabar();">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
                    </div>
                    <div class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_GenerarWIO" class="dv_img_boton" style="width: 130px; display: none;" runat="server">
                        <a href="javascript:fn_generarID();">
                            <img alt="" src="../Util/images/ico_acc_agregarID.gif" border="0" /><br />
                            Generar Ins. Desembolso </a>
                    </div>
                    <div id="dv_RecalculaIGV" class="dv_img_boton" style="width: 70px" runat="server">
                        <a href="javascript:fn_recalculaIGV();">
                            <img alt="" src="../Util/images/ico_acc_refrescar.gif" border="0" /><br />
                            Actualiza TC </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor">
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
                                    Datos del Contrato
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" value=""
                            runat="server" />
                        <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" value="" runat="server" />
                        <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" value=""
                            runat="server" />
                        <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" value=""
                            runat="server" />
                        <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" value="" runat="server" />
                        <div id="dvCabecera" class="dv_tabla_contenedora">
                            <table border="0" cellpadding="0" cellspacing="3" id="tb_formulario">
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
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNumeroContrato" name="txtNumeroContrato" runat="server" type="text"
                                            class="css_input_inactivo" size="11" value="" readonly="readonly" disabled="disabled" />
                                    </td>
                                    <td class="label">
                                        Estado
                                    </td>
                                    <td class="input">
                                        <input id="txtEstado" name="txtEstado" type="text" runat="server" class="css_input_inactivo"
                                            size="30" value="" readonly="readonly" disabled="disabled" />
                                        <asp:HiddenField ID="hidCodEstadoContrato" runat="server" />
                                    </td>
                                    <td class="label">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtclasificacion" name="txtclasificacion" runat="server" type="text" class="css_input_inactivo"
                                            size="40" value="" readonly="readonly" disabled="disabled" />
                                        <asp:HiddenField ID="hidCodClasificacion" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtcu" name="txtcu" type="text" runat="server" class="css_input_inactivo"
                                            size="11" value="" readonly="readonly" disabled="disabled" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" name="txtRazonSocial" runat="server" type="text" class="css_input_inactivo"
                                            size="40" value="" readonly="readonly" disabled="disabled" />
                                    </td>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <input id="txtmoneda" name="txtMoneda" type="text" runat="server" class="css_input_inactivo"
                                            size="20" value="" readonly="readonly" disabled="disabled" />
                                        <asp:HiddenField ID="hidCodMoneda" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Precio Venta
                                    </td>
                                    <td class="input">
                                        <input id="txtMontoFinanciado" name="txtcu0" type="text" runat="server" class="css_input_inactivo"
                                            size="11" value="" readonly="readonly" disabled="disabled" style="text-align: right" /><asp:HiddenField
                                                ID="hidMontoFinanciado" runat="server" />
                                    </td>
                                    <td class="label">
                                        IGV
                                    </td>
                                    <td class="input">
                                        <input id="txtMontoIgv" name="txtcu2" type="text" runat="server" class="css_input_inactivo"
                                            size="11" value="" readonly="readonly" disabled="disabled" style="text-align: right" />
                                        <asp:HiddenField ID="hidMontoIgv" runat="server" />
                                    </td>
                                    <td class="label">
                                        Valor Venta
                                    </td>
                                    <td class="input">
                                        <input id="txtCapitalFinanciado" name="txtcu1" type="text" runat="server" class="css_input_inactivo"
                                            size="11" value="" readonly="readonly" disabled="disabled" style="text-align: right" /><asp:HiddenField
                                                ID="hidCapitalFinanciado" runat="server" />
                                    </td>
                                </tr>
                                <tr>                                    
                                    <td class="label">
                                        Monto Total Registrado
                                    </td>
                                    <td class="input">
                                        <asp:HiddenField ID="hidNroInstruccion" runat="server" />
                                        <input id="txtMontoRegistrado" name="txtMontoRegistrado" type="text" runat="server" class="css_input_inactivo"
                                            size="11" value="" readonly="readonly" disabled="disabled" style="text-align: right" />
                                    </td>
                                    <td class="label">
                                        Por Desembolsar
                                    </td>
                                    <td class="input">
                                        <input id="txtMontoPorDesembolsar" name="txtMontoPorDesembolsar" type="text" runat="server" class="css_input_inactivo"
                                            size="11" value="" readonly="readonly" disabled="disabled" style="text-align: right" />                                        
                                    </td>
                                    <td class="label" id="lblChkActivacionLeasing">
                                        <asp:CheckBox ID="ChkActivacionLeasing" runat="server" />
                                        Activación Leasing
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaAct" name="txtFechaAct" runat="server" type="text" class="css_input"
                                            size="12" runat="server"  title="Fecha inicial de búsqueda del pago del impuesto"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:HiddenField ID="hidCodSubContrato" runat="server" />
                                        <asp:HiddenField ID="hidCodProductoActivo" runat="server" />
                                        <asp:HiddenField ID="hidNroLineaOp" runat="server" />
                                        <asp:HiddenField ID="hidPolizaSeguro" runat="server" />
                                        <asp:HiddenField ID="hidGastoActivacion" runat="server" />
                                        <asp:HiddenField ID="hidImporteOpCompra" runat="server" />
                                        <asp:HiddenField ID="hidPeriocidad" runat="server" />
                                        <asp:HiddenField ID="hidNroCuotas" runat="server" />
                                        <asp:HiddenField ID="hidRiesgoAsumido" runat="server" />
                                        <asp:HiddenField ID="hidImporteInicial" runat="server" />
                                        <asp:HiddenField ID="hidCodProcedenciaCotizacion" runat="server" />
                                        <asp:HiddenField ID="hdiCostoFondos" runat="server" />
                                        <asp:HiddenField ID="hdiTasa" runat="server" />
                                        <asp:HiddenField ID="hdiSpread" runat="server" />
                                        <asp:HiddenField ID="hidTipoComprobante" runat="server" />
                                        <asp:HiddenField ID="hidKeyTipoComprobante" runat="server" />
                                        <asp:HiddenField ID="hidKeyNumeroComprobante" runat="server" />
                                        <asp:HiddenField ID="hidKeyFechaEmision" runat="server" />
                                        <asp:HiddenField ID="hidKeyCodProveedor" runat="server" />
                                        <asp:HiddenField ID="hidTipoComNDNC" runat="server" />
                                        <asp:HiddenField ID="hidEstadoID" runat="server" />
                                        <!-- Inicio IBK -->
                                        <asp:HiddenField ID="hidNontoIGV" runat="server" />
                                        <input type="hidden" id="hidLazyLoadTab" name="hidLazyLoadTab" value="" runat="server" />
                                        <!-- Fin IBK -->
                                                                                
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                        
                        <!-- ******************************************************************************************** -->	
			            <!-- Inicia Tabs -->	
			            <!-- ******************************************************************************************** -->	        			    
	                    <div id="divTabs" style="border:0px; background:none;" class="dv_tabla_contenedora" >
			                <ul>				                
				                <li id="tab0-tab"><a href="#tab-0">Datos del Documento</a></li>
				                <li id="tab1-tab"><a href="#tab-1">Asociación Bienes</a></li>
			                </ul>
                        <!--
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Documento
                                </td>
                            </tr>
                        </table>
                        -->
                        <!--<div id="dv_datos" class="dv_tabla_contenedora">-->
                            <!-- ******************* -->	
        			        <!-- TAB :: DOCUMENTOS   -->	 
        			        <!-- ******************* -->	
					        <div id="tab-0" >
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
                                            <img id="imgBuscarProveedor" alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                                onclick="javascript:VentanaProveedores();" />
                                            <asp:HiddenField ID="hidCodProveedor" runat="server" />
                                        </td>
                                        <td class="label">
                                            Razón Social o Nombre
                                        </td>
                                        <td class="input">
                                            <input id="txtRazonSocialProveedor" name="txtRazonSocialProveedor" type="text" class="css_input_inactivo" size="45" disabled="disabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Tipo de Comprobante
                                        </td>
                                        <td class="input">
                                            <input id="txtNumeroTipo" name="txtNumeroTipo" type="text" class="css_input" runat="server"
                                                size="4" value="" />
                                            <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
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
                                            <input id="txtNumeroDoc1" name="txtNumeroDoc1" runat="server" type="text" class="css_input" size="20"
                                                value="" />
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblFecVenc" runat="server" Text="F.Pago" CssClass="label"></asp:Label>
                                        </td>
                                        <td class="input">
										    <span id="spa_FecPago">
										            <!-- Inicio IBK -->
											        <!--<input id="txtFechaVenc11" name="txtFechaVenc1" type="text" class="css_input" size="12"
												    runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />-->
												    <input id="txtFechaVenc1" name="txtFechaVenc1" type="text" class="css_input" size="12" onblur="fn_valFecha('2');"
												    runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />
												    <!-- Fin IBK -->
                                            </span>    
                                        </td>
                                    </tr>
                                    <tr id="trDUA" runat="server" style="display: none">
                                        <td class="label">
                                            Aduana
                                        </td>
                                        <td class="input">
                                            <input id="txtAduana" name="txtAduana" type="text" class="css_input" size="4" runat="server"
                                                value="" />
                                            <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                                runat="server" id="imgAduana" />
                                            <asp:Label ID="lblAduana" runat="server" class="label"></asp:Label>
                                        </td>
                                        <td class="label">
                                            Año
                                        </td>
                                        <td class="input">
                                            <input id="txtAnnoAduana" name="txtAnnoAduana" type="text" class="css_input" size="10"
                                                value="" />
                                        </td>
                                        <td class="label">
                                            Nº Comprobante
                                        </td>
                                        <td class="input">
                                            <input id="txtNumeroComprobante" name="txtNumeroComprobante" type="text" class="css_input"
                                                size="20" value="" />
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
                                                <input id="txtFechaEmision" name="txtFechaEmision" type="text" class="css_input" onblur="fn_valFecha('1');"
                                                    size="12" runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />
                                                <!-- Fin IBK -->    
                                        </td>
                                        <td class="label">
                                        </td>
                                        <td class="input">
                                            <select id="cmbProcedencia" name="cmbProcedencia" runat="server" style="display: none">
                                            </select>
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
                                        <input id="txtNumeroIGV" name="txtNumeroIGV" type="text" class="css_input"
                                                size="15" runat="server" style="text-align: right" />   
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
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="label"> 
										    <span id="spn_4taLabel">                           
											    Renta 4ta Categ.
                                            </span>
                                        </td>
                                        <td class="input">
										    <span id="spn_4taInput">
											    <input id="txtPorc4ta" name="txtPorc4ta" type="text" class="css_input" size="2" runat="server" />
											    %&nbsp;-&nbsp;&nbsp;S/.&nbsp;
											    <input id="txtMonto4taSoles" name="txtMonto4taSoles" type="text" class="css_input_inactivo" size="10" runat="server" disabled="disabled" style="text-align: right" />
											    &nbsp;$&nbsp;
											    <input id="txtMonto4taDolares" name="txtMonto4taDolares" type="text" class="css_input_inactivo" size="10" runat="server" disabled="disabled" style="text-align: right" />
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
                                            <input id="txttcdia" name="txttcdia" type="text" class="css_input" size="10" runat="server" style="text-align: right" />
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
                                            <input id="chkSunat" name="chkSunat" type="checkbox"  style="display:none"  runat="server" />T. C. Sunat
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
                                            <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
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
                                            <!--<input id="txtFechaCompro1" name="txtFechaEmision" type="text" class="css_input" size="12"
                                                runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />-->
                                            <input id="txtFechaCompro" name="txtFechaEmision" type="text" class="css_input" size="12"  onblur="fn_valFecha('3');"
                                                    runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />    
                                            <!-- Fin IBK -->
                                        </td>
                                    </tr>
                                    <tr id="trComprobante2" runat="server" style="display: none;">
                                        <td class="label">
                                            Tipo de Comprobante
                                        </td>
                                        <td class="input">
                                            <input id="txtNumeroTipo2" name="txtNumeroTipo2" type="text" class="css_input" size="4"
                                                value="" runat="server" />
                                            <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                                runat="server" id="imgNumeroTipo2" />
                                            <asp:Label ID="lblNumeroTipo2" runat="server" class="label"></asp:Label>
                                        </td>
                                        <td class="label">
                                            Nº Comprobante
                                        </td>
                                        <td class="input">
                                            <input id="txtSerieDoc2" name="txtSerieDoc2" type="text" class="css_input_inactivo"
                                                size="4" value="" runat="server" />
                                            &nbsp;-&nbsp;
                                            <input id="txtNroDoc2" name="txtNroDoc2" type="text" class="css_input_inactivo" size="15"
                                                value="" runat="server" />
                                            <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                                runat="server" id="imgNroDoc2"  onclick="javascript:fn_ventanaDocumento();" />
                                        </td>
                                        <td class="label">
                                            F. Emisión
                                        </td>
                                        <td class="input">
                                            <input id="txtFechaAdm" name="txtFechaAdm" type="text" class="css_input" size="12"
                                                runat="server" title="Fecha inicial de búsqueda del pago del impuesto" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td colspan="6" style="text-align: right; padding-right: 10px">
                                            <div id="dv_cancelar" class="dv_img_boton_mini" style="width: 80px; border: 0px solid #ffffff;"
                                                runat="server">
                                                <a href="javascript:fn_LimpiarDetalle();">
                                                    <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Cancelar </a>
                                            </div>
                                            <div id="dv_Modificar" class="dv_img_boton_mini" style="width: 80px; border: 0px solid #ffffff;"
                                                runat="server">
                                                <a href="javascript:fn_ModificarDetalle();">
                                                    <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Modificar </a>
                                            </div>
                                            <div id="dv_eliminar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
                                                dir="ltr">
                                                <a href="javascript:fn_EliminarDetalle();">
                                                    <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Anular </a>
                                            </div>
                                            <div id="dv_editar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;">
                                                <a href="javascript:fn_EditarDetalle();">
                                                    <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Editar </a>
                                            </div>
                                            <div id="dv_agregar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;">
                                                <a href="javascript:fn_AgregarDetalle();">
                                                    <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Agregar </a>
                                            </div>
                                            
                                            <div id="dv_EditarReplicar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;">
                                                <a href="javascript:fn_replicar();">
                                                    <img alt="" src="../Util/images/ico_acc_replicar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Replicar  </a>
                                            </div>
                                            
                                            <asp:HiddenField ID="hidTipoDocumento" runat="server" />
                                            <asp:HiddenField ID="hidNumeroDocumento" runat="server" />
                                            <asp:HiddenField ID="hidFecEmision" runat="server" />
                                            <asp:HiddenField ID="hidCodigoEstadoDoc" runat="server" />
                                            
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hidtotalDocs" runat="server" Value="0" />
                                <table id="jqGrid_lista_C">
                                    <tr>
                                        <td />
                                    </tr>
                                </table>
                                <%--<div id="jqGrid_pager_C">
                                </div>--%>
                                <div style="text-align: right;" id="divTotal">
                                   </div>
                            </div>
                            
                            
                            <!-- ******************************* -->    
                            <!-- TAB :: ASOCIACION               -->    
                            <!-- ******************************* -->    
                            <div id="tab-1">   
                            
                                <!--<table id="TbBienes" border="0" cellpadding="0" cellspacing="5" align="left" >-->
                                <table border="0" cellpadding="0" cellspacing="3" width="100%" id="tb_formulario">
                                    <tr>
                                        <td style="vertical-align:top">
                                            <fieldset>
                                            <legend  class="css_lbl_subTitulo">Bienes</legend>
                                                <table id="jqGrid_lista_A"><tr><td/></tr></table> 
                                                <div id="jqGrid_pager_A"></div>   
                                            </fieldset>
                                        </td>
                                        <td style="vertical-align:top">
                                            <fieldset>
                                            <legend  class="css_lbl_subTitulo">Documentos</legend>
                                                <table id="jqGrid_lista_B"><tr><td/></tr></table> 
                                                <div id="jqGrid_pager_B"></div>      
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td style="vertical-align:top">
                                            <input type="hidden" id="hidIdBien" name="hidIdBien" runat="server" value="" />     
                                            <input type="hidden" id="hidDocs" name="hidDocs" runat="server" value="" />     
                                        </td>
                                        <td align="left">  
                                            <div id="DvAgregarRel" class="dv_img_boton_mini" style="width: 120px; border: 0px solid #ffffff; ">
                                                <a href="javascript:fn_AgregarRelacion();">
                                                    <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Agregar Relacion </a>
                                            </div> 
                                            <div id="dv_deselAll" class="dv_img_boton_mini" style="width: 120px; border: 0px solid #ffffff; ">
                                                <a href="javascript:fn_DeSeleccionarTodo();">
                                                    <img alt="" src="../Util/images/DeSel_All.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Deseleccionar Todo </a>
                                            </div>      
                                             <div id="dv_selAll" class="dv_img_boton_mini" style="width: 120px; border: 0px solid #ffffff; ">
                                                <a href="javascript:fn_SeleccionarTodo();">
                                                    <img alt="" src="../Util/images/Sel_All.jpeg" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Seleccionar Todo </a>
                                            </div> 
                                            
                                                                 
                                        </td>
                                    </tr>                                        
                                    <tr>
                                        <td colspan="2" style="vertical-align:top">                                          
                                            <fieldset>
                                            <legend  class="css_lbl_subTitulo">Relaciones</legend>
                                                <table id="jqGrid_lista_D"><tr><td/></tr></table> 
                                                <div id="jqGrid_pager_D"></div>
                                            </fieldset>                                      
                                        </td>
                                    </tr>
                                </table> 
                            </div>
                            
                        </div>  
                    </td>
                </tr>
            </table>
        </div>
        <!-- Fin Cuerpo -->
    </div>
    <asp:HiddenField ID="hidTipoCambioDia" runat="server" />
    <asp:HiddenField ID="hidTipoCambioSunat" runat="server" />
    <asp:HiddenField ID="hidDesembolso" runat="server" />
    <asp:HiddenField ID="hidDesembolso1" runat="server" />
    <asp:HiddenField ID="hidTipoCambioDesembolso" runat="server" />
    <asp:HiddenField ID="hddSubtipoContrato" runat="server" />
    <asp:HiddenField ID="hddActionBotonGrid" runat="server" />
     <asp:HiddenField ID="hddFlagRegDesembolso" runat="server" />
     <input type="hidden" id="hddFechaActual" name="hddFechaActual" value="" runat="server" />
     <input type="hidden" id="hddtxttcdia" name="hddtxttcdia" value="" runat="server" />
     <input type="hidden" id="hddCodSolicitudCredito" name="hddCodSolicitudCredito" value="" runat="server" />
     
      
    </form>
</body>
</html>