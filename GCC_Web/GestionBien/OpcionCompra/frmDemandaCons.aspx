<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDemandaCons.aspx.vb" Inherits="GestionBien_OpcionBien_frmDemandaCons" %>

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
	<script type='text/javascript' src="frmDemandaCons.aspx.js"></script>
	
</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
		
		<input type="hidden" name="hddTipoTx" id="hddTipoTx" value="" runat="server" />
		<input type="hidden" name="hddTipoTxImplicado" id="hddTipoTxImplicado" value="" runat="server" />
		<input type="hidden" name="hddVer" id="hddVer" value="" runat="server" />
		
		<input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
		<input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />
		<input type="hidden" name="hddCodSiniestro" id="hddCodSiniestro" value="" runat="server" />		
		<input type="hidden" name="hddCodUnico" id="hddCodUnico" value="" runat="server" />
		
		<input type="hidden" name="hddCodDemanda" id="hddCodDemanda" value="" runat="server" />
		<input type="hidden" name="hddTipoSiniestro" id="hddTipoSiniestro" value="" runat="server" />
		
		<input type="hidden" name="hddCodSiniestroBsq" id="hddCodSiniestroBsq" value="" runat="server" />
		<input type="button" name="btnCargaSiniestro" id="btnCargaSiniestro" onclick="javascript:fn_aplicaBusquedaSiniestro();" style="display: none;" />
		
		<input type="hidden" name="hddCodImplicado" id="hddCodImplicado" value="" runat="server" />
		<input type="hidden" name="hddNroDocImplicado" id="hddNroDocImplicado" value="" runat="server" />
		<input type="hidden" name="hddTipoDocImplicado" id="hddTipoDocImplicado" value="" runat="server" />
		<input type="hidden" name="hddNombreImplicado" id="hddNombreImplicado" value="" runat="server" />
		<input type="hidden" name="hddTipoImplicado" id="hddTipoImplicado" value="" runat="server" />
		    
		<input type="hidden" name="hddNroSiniestro" id="hddNroSiniestro" value="" runat="server" />
    
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
						<div class="css_lbl_subTitulo">Gestión del Bien</div>
						<div class="css_lbl_titulo">Demanda :: Consulta</div>
					</td>
					<td class="espacio">&nbsp;</td>
					<td class="botones">
					
    						<div id="dv_img_boton" class="dv_img_boton">
								<a href="javascript:fn_cerrar();">
									<img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0"/><br />
									Cerrar
								</a>
							</div>			        
					</td>
				</tr>
			</table>
		    
			<div id="dv_contenedor" class="css_scrollPane">
		    
			<table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >
				<tr>
					<td class="lineas"></td>			
				</tr>
				<tr>
					<td class="cuerpo" style="padding-top: 0px;">
											
						<!-- ******************************************************************************************** -->	
						<!-- Inicia Tabs -->		
						<!-- ******************************************************************************************** -->	        			    
						<div id="divTabs" style="border:0px; background:none;" class="dv_tabla_contenedora" >
							<ul>
								<li><a href="#tab-0">SINIESTRO / DEMANDA</a></li>
								<li><a href="#tab-1">DEMANDA</a></li>
								<li id="tab2-tab"><a href="#tab-2">IMPLICADOS</a></li>
							</ul>

							<!-- **************** -->	
							<!-- TAB :: SINIESTRO   -->	 
							<!-- **************** -->	
							<div id="tab-0" >	        
							
								
								<table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
		                        
									<tr>
										<td class="label">
											Nº Siniestro
										</td>
										<td class="input">
											<input id="txtNroSiniestro" type="text" class="css_input_inactivo" readonly="true" runat="server" size="12" />
											</td>                                
										<td class="label">
											F. Conocimiento Banco
										</td>
										<td class="input">
											<input id="txtFechaConoBanco" type="text" class="css_input_inactivo" readonly="true" runat="server" size="15" />
										</td>                                
										<td class="label">
											F. Conocimiento Leasing
										</td>
										<td class="input">
											<input id="txtFechaConoLeasing" type="text" class="css_input_inactivo" readonly="true" runat="server" size="15" />
										</td>                                
									</tr>
		                        
									<tr>
										<td class="label">
											F. Siniestro
										</td>
										<td class="input">
											<input id="txtFechaSiniestro" type="text" class="css_input_inactivo" readonly="true" runat="server" size="15" />
										</td>                                
										<td class="label">
											Tipo
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbTipo" name="cmbTipo" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>
										</td>                                
		                                                             
									</tr>
		                            
									<tr>
										<td class="label">
											F. última actualización
										</td>
										<td class="input">
											<input id="txtFechaSituacion" type="text" class="css_input_inactivo" readonly="true" runat="server" size="10" />
										</td>   
										<td class="label">
											Situación
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbSituacion" name="cmbSituacion" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>
										</td>                                
										<td class="label">
											Modificación de Contrato
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbContrato" name="cmbContrato" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>
										</td>                                
		                                                               
									</tr>
		                            
									<tr>
										<td class="label">
											F. Aplicación
										</td>
										<td class="input">
											<input id="txtFechaAplicacion" type="text" class="css_input_inactivo" readonly="true" runat="server" size="15" />                                   
										</td> 
										<td class="label">
											Aplicación
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbAplicacion" name="cmbAplicacion" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>	                                   
										</td>                                
										<td class="label">
											F. Descargo Municipal
										</td>
										<td class="input">
											<input id="txtFechaDescargoMunicipal" type="text" class="css_input_inactivo" readonly="true" runat="server" size="15" />
										</td>                                
		                                                              
									</tr>
		                            
									<tr>
										<td class="label">
											Venta a Compañía de Seguro
										</td>
										<td class="input">
											<input id="txtFechaTransparencia" type="text" class="css_input_inactivo" readonly="true" runat="server" size="15" />
										</td>  
										<td class="label">
											Transferencia del Bien
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbTransferencia" name="cmbTransferencia" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>	
										</td>                                
										<td class="label">
											Origen Conocimiento
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbOrigenCono" name="cmbOrigenCono" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>	                                  
										</td>                                                      
									</tr>
		                            
									<tr>
										<td class="label">
											Seguro
										</td>
										<td class="input" colspan="3">
											<select disabled="disabled" id="cmbSeguro" name="cmbSeguro" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>	                                  
										</td>  
										<td class="label">
											Nº Cheque de Aseguradora
										</td>
										<td class="input">
											<input id="txtChequeAseg" type="text" class="css_input_inactivo" readonly="true" runat="server" size="20" />	
										</td>                         
									</tr>
		                            
									<tr>
										<td class="label">
											Estado del Bien
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbEstadoBien" name="cmbEstadoBien" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>
										</td>   
										<td class="label">
											Nº Poliza
										</td>
										<td class="input">
											<input id="txtNroPoliza" type="text" class="css_input_inactivo" readonly="true" runat="server" size="20" />	
										</td>  
										<td class="label">
											Tipo de Póliza
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbTipoPoliza" name="cmbTipoPoliza" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>	                                   
										</td>                         
									</tr>
		                            
									<tr>
										<td class="label">
											F. Rec Indemnización
										</td>
										<td class="input">
											<input id="txtFechaIndem" type="text" class="css_input_inactivo" readonly="true" runat="server" size="15" />	                                   
										</td>   
										<td class="label">
											Moneda Indemnización
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbMonedaIndem" name="cmbMonedaIndem" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>	                                   
										</td>  
										<td class="label">
											Monto Indemnización
										</td>
										<td class="input">
											<input id="txtMontoIndem" type="text" class="css_input_inactivo" style="text-align:right" readonly="true" runat="server" size="15" />	
										</td>                                                                                        
									</tr>
		                            
									<tr>
										 <td class="label">
											Banco que emite el cheque
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbBancoEmite" name="cmbBancoEmite" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>		                                   
										</td> 
										<td class="label" style="display:none;">
											Aplicación del Fondo
										</td>
										<td class="input" style="display:none;">
											<select disabled="disabled" id="cmbAplicaFondo" name="cmbAplicaFondo" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>		                                   
										</td> 
									</tr>
										
									<tr id="tr_cuenta" style="display:none;">								
										<td class="label">
											Nº Cuenta
										</td>
										<td class="input">
											<input id="txtNroCuenta" type="text" class="css_input_inactivo" readonly="true" runat="server" size="13" />		                                   
										</td>                                
										<td class="label">
											Tipo Cuenta
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbTipoCuenta" name="cmbTipoCuenta" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>		                                   
										</td>                                
										<td class="label">
											Moneda
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbMonedaCuenta" name="cmbMonedaCuenta" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>		                                   
										</td>                                
									</tr>
		                            
								</table>       
							
							</div>
							
							
							<!-- **************** -->	
							<!-- TAB :: DEMANDA -->	 
							<!-- **************** -->	
							<div id="tab-1">
								
								<table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
		                        
									<tr>
										<td class="label">
											Nº Demanda
										</td>
										<td class="input">
											<input id="txtNroDemanda" type="text" class="css_input_inactivo" readonly="true" runat="server" size="12" />
										</td>                                
										<td class="label">
											F. Demanda
										</td>
										<td class="input">
											<input id="txtFechaDemanda" type="text" class="css_input_inactivo" readonly="true" runat="server" size="15" />
										</td>                                
										<td class="label">
											Estado Demanda
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbEstadoDemanda" name="cmbEstadoDemanda" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>	
										</td>                                
									</tr>
		                            
									<tr>
										<td class="label">
											Juzgado
										</td>
										<td class="input">
											<input id="txtJuzgado" type="text" class="css_input_inactivo" readonly="true" runat="server" size="30" />
										</td>                                
										<td class="label">
											Moneda Demanda
										</td>
										<td class="input">
											<select disabled="disabled" id="cmbMonedaDemanda" name="cmbMonedaDemanda" runat="server">	
												<option value="0"> - Seleccionar -</option>       
											</select>	
										</td>                                
										<td class="label">
											Monto Demanda
										</td>
										<td class="input">
											<input id="txtMontoDemanda" type="text" class="css_input_inactivo" style="text-align:right" readonly="true" runat="server" size="15" />
										</td>                                
									</tr>
		                            		                            
								</table>   
							
							</div>
							
							<!-- ******************************* -->	
							<!-- TAB :: IMPLICADOS -->	
							<!-- ******************************* -->	
							<div id="tab-2">					
								<table id="jqGrid_lista_A">
									<tr><td /></tr>
								</table>
								<div id="jqGrid_pager_A"></div>																	
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
