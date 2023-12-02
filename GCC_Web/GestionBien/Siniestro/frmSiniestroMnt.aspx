<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiniestroMnt.aspx.vb" Inherits="GestionBien_Siniestro_frmSiniestroMnt" %>

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
	
	<!-- Local -->	
	<script type='text/javascript' src="frmSiniestroMnt.aspx.js"></script>
	
</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
    
		<input type="hidden" name="hddTipoTx" id="hddTipoTx" value="" runat="server" />
		<input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
		<input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />
		<input type="hidden" name="hddCodSiniestro" id="hddCodSiniestro" value="" runat="server" />
		<input type="hidden" name="hddCodUnico" id="hddCodUnico" value="" runat="server" />
		<input type="hidden" name="hddVer" id="hddVer" value="" runat="server" />
    
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
            <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    	    <tr>
			    <td class="icono">
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Util/images/ico_mdl_siniestro.gif" />
                </td>
			    <td class="titulos" style="width:250px;">
				    <div class="css_lbl_subTitulo">Gestión del Bien</div>
				    <div class="css_lbl_titulo">Siniestro :: <span id="sp_accion">Mantenimiento</span> </div>
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
				        
				        <div id="dv_separador" class="dv_img_boton_separador">
							:
						</div>
                    		        
						<div id="dv_documentos" class="dv_img_boton" style="width:80px;">
					        <a href="javascript:fn_GBAbreDocumentos();">
						        <img alt="" src="../../Util/images/ico_version.gif" border="0"/><br />
						        Documentos
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
                                    Nº Siniestro
                                </td>
                                <td class="input">
									<input id="txtNroSiniestro" type="text" class="css_input" runat="server" size="12" />
                                </td>                                
                                <td class="label">
                                    F. Conocimiento Banco
                                </td>
                                <td class="input">
									<input id="txtFechaConoBanco" type="text" class="css_input" runat="server" size="15" />
                                </td>                                
                                <td class="label">
                                    F. Conocimiento Leasing
                                </td>
                                <td class="input">
									<input id="txtFechaConoLeasing" type="text" class="css_input" runat="server" size="15" />
                                </td>                                
                            </tr>
                        
							<tr>
                                <td class="label">
                                    F. Siniestro
                                </td>
                                <td class="input">
									<input id="txtFechaSiniestro" type="text" class="css_input" runat="server" size="15" />
                                </td>                                
                                <td class="label">
                                    Tipo
                                </td>
                                <td class="input">
									<select id="cmbTipo" name="cmbTipo" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>
                                </td>                                
                                                             
                            </tr>
                            
                            <tr>
								<td class="label">
                                    F. última actualización
                                </td>
                                <td class="input">
									<input id="txtFechaSituacion" type="text" class="css_input" runat="server" size="10" />
                                </td>   
                                <td class="label">
                                    Situación
                                </td>
                                <td class="input">
									<select id="cmbSituacion" name="cmbSituacion" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>
                                </td>                                
                                <td class="label">
                                    Modificación de Contrato
                                </td>
                                <td class="input">
									<select id="cmbContrato" name="cmbContrato" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>
                                </td>                                
                                                               
                            </tr>
                            
                            <tr>
								<td class="label">
                                    F. Aplicación
                                </td>
                                <td class="input">
									<input id="txtFechaAplicacion" type="text" class="css_input" runat="server" size="15" />                                   
                                </td> 
                                <td class="label">
                                    Aplicación
                                </td>
                                <td class="input">
									<select id="cmbAplicacion" name="cmbAplicacion" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>	                                   
                                </td>                                
                                <td class="label">
                                    F. Descargo Municipal
                                </td>
                                <td class="input">
									<input id="txtFechaDescargoMunicipal" type="text" class="css_input" runat="server" size="15" />
                                </td>                                
                                                              
                            </tr>
                            
                            <tr>
								<td class="label">
                                    Venta a Compañía de Seguro
                                </td>
                                <td class="input">
									<input id="txtFechaTransparencia" type="text" class="css_input" runat="server" size="15" />
                                </td>  
                                <td class="label">
                                    Transferencia del Bien
                                </td>
                                <td class="input">
									<select id="cmbTransferencia" name="cmbTransferencia" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>	
                                </td>                                
                                <td class="label">
                                    Origen Conocimiento
                                </td>
                                <td class="input">
									<select id="cmbOrigenCono" name="cmbOrigenCono" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>	                                  
                                </td>                                                      
                            </tr>
                            
                            <tr>
								<td class="label">
                                    Seguro
                                </td>
                                <td class="input" colspan="3">
									<select id="cmbSeguro" name="cmbSeguro" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>	                                  
                                </td>  
                                <td class="label">
                                    Nº Cheque de Aseguradora
                                </td>
                                <td class="input">
									<input id="txtChequeAseg" type="text" class="css_input" runat="server" size="20" />	
                                </td>                         
                            </tr>
                            
                            <tr>
								<td class="label">
                                    Estado del Bien
                                </td>
                                <td class="input">
									<select id="cmbEstadoBien" name="cmbEstadoBien" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>
                                </td>   
								<td class="label">
                                    Nº Poliza
                                </td>
                                <td class="input">
									<input id="txtNroPoliza" type="text" class="css_input" runat="server" size="20" />	
                                </td>  
                                <td class="label">
                                    Tipo de Póliza
                                </td>
                                <td class="input">
									<select id="cmbTipoPoliza" name="cmbTipoPoliza" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>	                                   
                                </td>                         
                            </tr>
                            
                            <tr>
								<td class="label">
                                    F. Rec Indemnización
                                </td>
                                <td class="input">
									<input id="txtFechaIndem" type="text" class="css_input" runat="server" size="15" />	                                   
                                </td>   
								<td class="label">
                                    Moneda Indemnización
                                </td>
                                <td class="input">
									<select id="cmbMonedaIndem" name="cmbMonedaIndem" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>	                                   
                                </td>  
                                <td class="label">
                                    Monto Indemnización
                                </td>
                                <td class="input">
									<input id="txtMontoIndem" type="text" class="css_input" runat="server" size="15" />	
                                </td>                                                                                        
                            </tr>
                            
                            <tr>
								 <td class="label">
                                    Banco que emite el cheque
                                </td>
                                <td class="input">
									<select id="cmbBancoEmite" name="cmbBancoEmite" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>		                                   
                                </td> 
								<td class="label" style="display:none;">
                                    Aplicación del Fondo
                                </td>
                                <td class="input" style="display:none;">
									<select id="cmbAplicaFondo" name="cmbAplicaFondo" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>		                                   
                                </td> 
                            </tr>
								
                            <tr id="tr_cuenta" style="display:none;">								
                                <td class="label">
                                    Nº Cuenta
                                </td>
                                <td class="input">
									<input id="txtNroCuenta" type="text" class="css_input" runat="server" size="13" />		                                   
                                </td>                                
                                <td class="label">
                                    Tipo Cuenta
                                </td>
                                <td class="input">
									<select id="cmbTipoCuenta" name="cmbTipoCuenta" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>		                                   
                                </td>                                
                                <td class="label">
                                    Moneda
                                </td>
                                <td class="input">
									<select id="cmbMonedaCuenta" name="cmbMonedaCuenta" runat="server">	
										<option value="0"> - Seleccionar -</option>       
                                    </select>		                                   
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
