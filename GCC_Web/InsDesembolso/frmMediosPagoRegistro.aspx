<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMediosPagoRegistro.aspx.vb" Inherits="InsDesembolso_frmMediosPagoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documentos y Comentarios</title>
    
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

	<!-- Local -->	
	<script type='text/javascript' src="frmMediosPagoRegistro.aspx.js"></script>
	
</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
        
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
            <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    	    <tr>
			    <td class="icono">
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Util/images/ico_mdl_demanda.gif" />
                </td>
			    <td class="titulos" style="width:250px;">
				    <div class="css_lbl_subTitulo">Instrucción de Desembolso</div>
				    <div class="css_lbl_titulo">Medios de Pago</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
    				    <div id="dv_img_boton" class="dv_img_boton">
					        <a href="javascript:parent.fn_util_CierraModal();">
						        <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						        Cerrar
					        </a>
				        </div>
    				    <div id="Div1" class="dv_img_boton_separador">
		    		        :
	    			    </div>			
			    	    <div id="dv_img_boton_g" class="dv_img_boton">
					        <a href="javascript:fn_guardar();">
						        <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
						        Grabar
					        </a>
				        </div>
			    </td>
		    </tr>
	    </table>
	    
	    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
		    <tr>
				<td class="cuerpo">
					
					<input type="hidden" name="hddTipoTx" id="hddTipoTx" value="" runat="server" />
					<input type="hidden" name="hddCodProveedor" id="hddCodProveedor" value="" runat="server" />
					<input type="hidden" name="hddCodigoInsDesembolso" id="hddCodigoInsDesembolso" value="" runat="server" />
                    <input type="hidden" name="hddCodigoContrato" id="hddCodigoContrato" value="" runat="server" />
                    <input type="hidden" name="hddCodigoAgrupacion" id="hddCodigoAgrupacion" value="" runat="server" />
                    <input type="hidden" name="hddCodigoMonedaAgrupacion" id="hddCodigoMonedaAgrupacion" value="" runat="server" />
                    <input type="hidden" name="hddCodigoGrupoHtml" id="hddCodigoGrupoHtml" value="" runat="server" />
                    <input type="hidden" name="hddValidaCuenta" id="hddValidaCuenta" value="" runat="server" />
                    <input type="hidden" name="hddcu" id="hddcu" value="" runat="server" />
                     <input type="hidden" name="hddAccion" id="hddAccion" value="" runat="server" />
                    <!-- Inicio IBK - AAE - Agrego el estado -->
                    <input type="hidden" name="hddCodEstadoEjecucion" id="hddCodEstadoEjecucion" value="" runat="server" />
                    <input type="hidden" name="hddCodMonedaContrato" id="hddCodMonedaContrato" value="" runat="server" />
                    <input type="hidden" name="hddPendiente03" id="hddPendiente03" value="" runat="server" />
                    <input type="hidden" name="hddEmisora03" id="hddEmisora03" value="" runat="server" />
                    <input type="hidden" name="hddReceptora03" id="hddReceptora03" value="" runat="server" />
                    <input type="hidden" name="hddPendiente04" id="hddPendiente04" value="" runat="server" />
                    <input type="hidden" name="hddEmisora04" id="hddEmisora04" value="" runat="server" />
                    <input type="hidden" name="hddReceptora04" id="hddReceptora04" value="" runat="server" />
                    <input type="hidden" name="hddPendiente05" id="hddPendiente05" value="" runat="server" />
                    <input type="hidden" name="hddEmisora05" id="hddEmisora05" value="" runat="server" />
                    <input type="hidden" name="hddReceptora05" id="hddReceptora05" value="" runat="server" />
                    <!-- Fin IBK  -->
                    
                    
                    <input type="hidden" name="hddCodigoMedioPago" id="hddCodigoMedioPago" value="" runat="server" />
                        
					<div class="dv_tabla_contenedora">
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="label">
                                    MedioAbono
                                </td>
                                <td class="input">
                                    <select id="cmbMedio" name="cmbMedio" runat="server">
										<option value = "0">[ Seleccione ]</option>
                                    </select>
                                </td>                                
                            </tr>
                            <tr id="tr_grupo1">
                                <td class="label">
                                    Moneda Cuenta
                                </td>
                                <td class="input">
                                    <select id="cmbMonedaCuenta" name="cmbMonedaCuenta" runat="server">
										<option value="0">[ Seleccione ]</option>
                                    </select>
                                </td>
                                 <td class="label">
                                    Tipo Cuenta
                                </td>
                                <td class="input">
                                    <select id="cmbTipoCuenta" name="cmbTipoCuenta" runat="server">
										<option value="0">[ Seleccione ]</option>
                                    </select>
                                </td>  
                                 <td class="label">
                                    Número Cuenta
                                </td>
                                <td class="input">
                                    <input id="txtNroCuenta" type="text" size="20" class="css_input" runat="server" />
                                </td>                                  
                            </tr>
                            <tr id="tr_grupo2">                                    
                                <td class="label">
                                    Cuenta Bancaria
                                </td>
                                <td class="input">
                                    <input id="txtCuentaBancaria" type="text" size="25" class="css_input" runat="server" />
                                </td>                                 
                            </tr>
                            <tr id="tr_grupo3_1">                                    
                                <td class="label">
                                    Pendiente
                                </td>
                                <td class="input">
                                    <input id="txtPendiente" type="text" size="20" class="css_input" runat="server" />
                                </td>
                                <td class="label">
                                    Moneda
                                </td>
                                <td class="input">
                                    <select id="cmbMonedaPend" name="cmbMonedaPend" runat="server">
										<option value="0">[ Seleccione ]</option>
                                    </select>
                                </td>
                                <td class="label">
                                    Nota
                                </td>
                                <td class="input">
                                    <input id="txtNota" type="text" size="16" class="css_input" runat="server" />
                                </td>             
                            </tr>
                            <tr id="tr_grupo3_2">
                                <td class="label">
                                    Emisora
                                </td>
                                <td class="input">
                                    <input id="txtEmisora" type="text" size="20" class="css_input" runat="server" />
                                </td>
                                <td class="label">
                                    Receptora
                                </td>
                                <td class="input">
                                    <input id="txtReceptora" type="text" size="20" class="css_input" runat="server" />
                                </td>             
                            </tr>
                            <tr id="tr_grupo4_1">
                                <td class="label">
                                    Nro. Documento
                                </td>
                                <td class="input">
                                    <input id="txtNroDocumento" type="text" size="20" class="css_input" runat="server" />
                                </td>
                                <td class="label">
                                    Tipo Documento
                                </td>
                                <td class="input">
                                    <select id="cmbTipoDocumento" name="cmbTipoDocumento" runat="server">
										<option value="0">[ Seleccione ]</option>
                                    </select>
                                </td>                                      
                            </tr>
                            <tr id="tr_grupo4_2">                               
                                <td class="label">
                                    Razón Social o Nombre
                                </td>
                                <td class="input" colspan="5">
                                    <input id="txtRazonSocial" type="text" size="80" class="css_input" runat="server" />
                                </td>        
                            </tr>
                            <tr id="tr_grupo5">
                                <td class="label">
                                    Pago Comisión
                                </td>
                                <td class="input">
									<select id="cmbPagoComision" name="cmbPagoComision" runat="server">
										<option value="0">[ Seleccione ]</option>
                                    </select>                                    
                                </td>
                                <td class="label">
                                    Monto Comisión
                                </td>
                                <td class="input">
                                    <input id="txtMontoComision" type="text" size="20" class="css_input" runat="server" />
                                </td>
                            </tr>
                            <!-- Inicio IBK - AAE - Agrego abonos y cargos no dom -->
                            <tr id="tr_grupo6_1">                                    
                                <td class="label">
                                    Cargo:
                                </td>
                                <td class="input">
                                    <input id="txtCargoNoDom" type="text" size="25" class="css_input" runat="server" />
                                </td>
                                <td class="label">
                                    Cuenta:
                                </td>
                                <td class="input">
                                    <input id="txtCtaCargoNoDom" type="text" size="25" class="css_input" runat="server" />
                                </td>                                 
                            </tr>
                            <tr id="tr_grupo6_2">                                    
                                <td class="label">
                                    Abono:
                                </td>
                                <td class="input">
                                    <input id="txtAbonoNoDom" type="text" size="25" class="css_input" runat="server" />
                                </td>
                                <td class="label">
                                    Cuenta:
                                </td>
                                <td class="input">
                                    <input id="txtCtaAbonoNoDom" type="text" size="25" class="css_input" runat="server" />
                                </td>                                 
                            </tr>
                            <!-- Fin IBK -->                            
                        </table>                                                      
                    </div>
										
				</td>
		    </tr>
	    </table>
	    
      </div>
      
      
      
    </form>
</body>
</html>
