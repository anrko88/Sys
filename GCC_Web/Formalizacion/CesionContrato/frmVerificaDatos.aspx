<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVerificaDatos.aspx.vb" Inherits="Formalizacion_CesionContrato_frmVerificaDatos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documentos y Comentarios</title>
    
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
        
    <script type="text/javascript" src="../../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmVerificaDatos.aspx.js"></script>
	
</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
    
		<input type="hidden" name="hddTipoTx" id="hddTipoTx" value="" runat="server" />
		<input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
		    
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
            <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    	    <tr>
			    <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_cesion.gif" />
                </td>
			    <td class="titulos" style="width:450px;">
				    <div class="css_lbl_subTitulo">Formalización</div>
				    <div class="css_lbl_titulo">Cesión de Contrato :: Verifica Datos </div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
			    
    				    <div id="dv_img_boton" class="dv_img_boton">
					        <a href="javascript:parent.fn_util_CierraModal();">
						        <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						        Cerrar
					        </a>
				        </div>		
				        
				        <div id="dv_guardar" class="dv_img_boton">
							<a href="javascript:fn_guardar();">
								<img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0"/><br />
								Guardar
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
										
					<div class="dv_tabla_contenedora" style="padding-top:0px;">
                        
						<fieldset>
								<legend class="css_lbl_subTitulo">Datos del Cesionario</legend>
								    
								<table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
		                        
									<tr>
										<td class="label">
											CU Cliente
										</td>
										<td class="input">
											<input id="txtNroDocumento" type="text" class="css_input" size="11" />
										</td>
										<td class="label">
											Razon Social
										</td>
										<td class="input">
											<input id="txtNroDocumento" type="text" class="css_input_inactivo" size="50" />
										</td>                                
									</tr>
		                            
									<tr>
										<td class="label">
											Banca
										</td>
										<td class="input">
											<input id="txtNroDocumento" type="text" class="css_input_inactivo" size="20" />
										</td>
										<td class="label" style="width:120px;">
											Dirección
										</td>
										<td class="input" colspan="5">
											<input id="txtRazSocial" type="text" class="css_input_inactivo" runat="server" size="70" />
										</td>                                                                                             
									</tr>
		                            
								</table>                                                      
		                  
		                  </fieldset>
		                        
						  <fieldset>
								<legend class="css_lbl_subTitulo">Cuentas</legend>
								    
								<table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
		                        
									<tr>
										<td class="label">
											Tipo Cuenta
										</td>
										<td class="input">
											<select id="cmdTipoCuenta1" name="cmdTipoDoc" runat="server">
											</select>
										</td>
										<td class="label">
											Moneda
										</td>
										<td class="input">
											<select id="cmdMoneda1" name="cmdTipoDoc" runat="server">
											</select>
										</td>
										<td class="label">
											Nº Cuenta
										</td>
										<td class="input">
											<input id="txtNroCuenta1" type="text" class="css_input" size="20" />
										</td>                                
									</tr>
									
									<tr>
										<td class="label">
											Tipo Cuenta
										</td>
										<td class="input">
											<select id="cmdTipoCuenta2" name="cmdTipoDoc" runat="server">
											</select>
										</td>
										<td class="label">
											Moneda
										</td>
										<td class="input">
											<select id="cmdMoneda2" name="cmdTipoDoc" runat="server">
											</select>
										</td>
										<td class="label">
											Nº Cuenta
										</td>
										<td class="input">
											<input id="txtNroCuenta2" type="text" class="css_input" size="20" />
										</td>                                
									</tr>
		                            
								</table>                                                      
		                  
		                  </fieldset>
		                        		                        			                        
                    </div>
                    
                    
                    	
				</td>
		    </tr>
	    </table>
	    
      </div>
      
      
      
    </form>
</body>
</html>
