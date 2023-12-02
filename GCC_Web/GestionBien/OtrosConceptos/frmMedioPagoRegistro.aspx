<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMedioPagoRegistro.aspx.vb" Inherits="GestionBien_OtrosConcepto_frmMedioPagoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Medio de Pago</title>
    
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
    <script type="text/javascript" src="../../Util/js/jquery/json2.js"></script>
    <script type="text/javascript" src="../../Util/js/js_global.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.modal.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.date.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"> </script>
    <script src="../../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../../Util/js/jquery/jshashtable.js" type="text/javascript"></script>

	<!-- Local -->	
	<script type='text/javascript' src="frmMedioPagoRegistro.aspx.js"></script>
	
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
				    <div class="css_lbl_subTitulo">Instrucción Gasto</div>
				    <div class="css_lbl_titulo">Medios de Pago</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
    				    <div id="dv_img_boton" class="dv_img_boton">
					        <a href="javascript:parent.fn_util_CierraModal();">
						        <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						        Cerrar
					        </a>
				        </div>
    				    <div id="Div1" class="dv_img_boton_separador">
		    		        :
	    			    </div>			
			    	    <div id="dv_img_boton_g" class="dv_img_boton">
					        <a href="javascript:fn_guardar();">
						        <img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0"/><br />
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
					<div class="dv_tabla_contenedora">
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="label">
                                    Medio Pago
                                </td>
                                <td class="input">
                                    <select id="cmbMedio" name="cmbMedio" runat="server">
										<option value = "0">[-Seleccione-]</option>
										<option value = "002">CUENTA</option>
										<option value = "007">CONTABLE</option>
                                    </select>
                                </td>                                
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
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
                            <tr id="tr_grupo3">
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
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            </table>                                                      
                    </div>
										
				</td>
		    </tr>
	    </table>
	    
      </div>
      
      
      
    </form>
</body>
</html>
