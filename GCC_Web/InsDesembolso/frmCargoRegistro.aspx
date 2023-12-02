<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCargoRegistro.aspx.vb" Inherits="InsDesembolso_frmCargoRegistro" %>

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
	<script type='text/javascript' src="frmCargoRegistro.aspx.js"></script>
	
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
				    <div class="css_lbl_titulo">Cargos</div>
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
			    	    <div id="dv_img_boton" class="dv_img_boton">
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
					<input type="hidden" name="hddCodigoInsDesembolso" id="hddCodigoInsDesembolso" value="" runat="server" />
                    <input type="hidden" name="hddCodigoContrato" id="hddCodigoContrato" value="" runat="server" />
                    <input type="hidden" name="hddCodMonedaContrato" id="hddCodMonedaContrato" value="" runat="server" />                    
                    
                    <input type="hidden" name="hddMontoTotal" id="hddMontoTotal" value="" runat="server" /> 
                    
                    <input type="hidden" name="hddPorcComActivacion" id="hddPorcComActivacion" value="" runat="server" />
                    <input type="hidden" name="hddPorcComEstructuracion" id="hddPorcComEstructuracion" value="" runat="server" />
                    <input type="hidden" name="hddPorcCuotaInicial" id="hddPorcCuotaInicial" value="" runat="server" />
                    
                    <input type="hidden" name="hddRdComActivacion" id="hddRdComActivacion" value="" runat="server" />
                    <input type="hidden" name="hddRdComEstructuracion" id="hddRdComEstructuracion" value="" runat="server" />
                    <input type="hidden" name="hddRdCuotaInicial" id="hddRdCuotaInicial" value="" runat="server" />
                    
                    <input type="hidden" name="hddMontoComActivacion" id="hddMontoComActivacion" value="" runat="server" />
                    <input type="hidden" name="hddMontoComEstructuracion" id="hddMontoComEstructuracion" value="" runat="server" />
                    <input type="hidden" name="hddMontoCuotaInicial" id="hddMontoCuotaInicial" value="" runat="server" />
                        
					<div class="dv_tabla_contenedora">
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="label">
                                    Concepto
                                </td>
                                <td class="input">
                                    <select id="cmbConcepto" name="cmbConcepto" runat="server">
										<option value = "0">[ Seleccione ]</option>
                                    </select>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="label">
                                    Moneda Contrato
                                </td>
                                <td class="input">
                                    <select id="cmbMoneda" name="cmbMoneda" runat="server">
										<option value="0">[ Seleccione ]</option>
                                    </select>
                                </td>                                
                            </tr>
                            <tr id="tr_Porc">                                    
                                <td class="label">
                                    Porcentaje
                                </td>
                                <td class="input">
                                    <!-- Inicio IBK - AAE -->
                                    <!--<input id="txtPorc_" type="text" size="8" class="css_input_inactivo" runat="server" disabled="disabled" />%-->
                                    <input id="txtPorc" type="text" size="8" class="css_input" runat="server"/>%
                                    <!-- Fin IBK -->
                                </td>                                 
                            </tr>
                            <tr>                                    
                                <td class="label">
                                    Monto
                                </td>
                                <td class="input">
                                    <input id="txtMonto" type="text" size="12" class="css_input" runat="server" />
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
