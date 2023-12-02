<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmContratoVer.aspx.vb" Inherits="Formalizacion_frmContratoVer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <title>Cotizacion Ver</title>
    
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />	
	
	<!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
	<link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css" media="all" />
	<link type="text/css" rel="stylesheet" href="../Util/css/css_global.css" />
	<link type="text/css" rel="stylesheet" href="../Util/css/css_formulario.css" />
	<link type="text/css" rel="stylesheet" href="../Util/css/css_fuente.css" />
		
	<!-- JavaScript -->
    <script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"></script>
	
	<script type='text/javascript' src="../Util/js/jquery/jquery.ui.global.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.validText.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.validNumber.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.maxlength.js"></script>
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.date.js"></script>
    <script type='text/javascript' src="../Util/js/js_util.ajax.js"></script>
    
    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>
    
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

	<!-- Local -->	
	<script type='text/javascript' src="frmContratoVer.aspx.js"></script>
	
</head>
<body>
    <form id="frmContratoVer" runat="server">
        
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo" >
    	<!--
        BOTONES
        -->  				
	    <table id="tb_cuerpoCabecera" style="border: 0;" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="icono">
				    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo">Formalización</div>
				    <div class="css_lbl_titulo">Contrato :: Editar</div>
			    </td>
			    <td class="espacio">
			        &nbsp;    								
			    </td>
			    <td class="espacio">
				    &nbsp; 
			    </td>
		    </tr>
	    </table>
	    
	    <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" value="" runat="server" />
        <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" value="" runat="server" />
        <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" value="" runat="server" />
        <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" value="" runat="server" />
        <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" value="" runat="server" />
	     	
	    <div id="dv_contenedor" class="css_scrollPane">			
	        <table id="tb_tabla_comun" cellpadding="0" cellspacing="0" style="width:100%; border:0;">
		        <tr>
			        <td class="lineas"></td>
		        </tr>
		        <tr>
			        <td class="cuerpo">
        				<!--
        				CABECERA DEL CONTRATO
        				-->  				
				        <table id="tb_tabla_comunCabecera" cellpadding="0" cellspacing="0" style="border:0;">
					        <tr>
						        <td class="titulo css_lbl_tituloContenido">Datos del Contrato</td>
						        <td class="botones">
							        <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						        </td>
					        </tr>
				        </table>      
				        <div id="dv_datos" class="dv_tabla_contenedora">
					        <table id="tb_formulario" style="border:0;" cellpadding="0" cellspacing="3">
					            <colgroup>                           
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                </colgroup>
					            
                                <tr>
						            <td class="label">Nº Contrato</td>
							        <td class="input"><input id="txtNroContrato" type="text" class="css_input_inactivo" readonly="readonly" style="width:100px;" runat="server" /></td>
	    					        <td class="label">Estado del Contrato</td>
							        <td class="input">
                                        <input id="txtEstadoDelContrato" name="txtEstadoDelContrato" type="text" class="css_input_inactivo" readonly="readonly" style="width:160px;" runat="server" />
							        </td>
                                    <td class="label">F. Contrato</td>
						            <td class="input"><input id="txtFechaContrato" name="txtFechaContrato" type="text" class="css_input_inactivo" readonly="readonly" style="width:100px;" runat="server" /></td>
 						        </tr>
    						    <tr>	
						            <td class="label">Fecha Máx. de Disponibilidad</td>
							        <td class="input"><input id="txtFechamaxdisp" name="txtFechamaxdisp" type="text" class="css_input_inactivo" style="width:100px;" runat="server" /></td>
							        <td class="label">Fecha Máx. de Activación</td>
							        <td class="input"><input id="txtFechaActivacion" name="txtFechaActivacion" type="text" class="css_input_inactivo" runat="server" style="width:100px;" /></td>	
                                    <td class="label">Periodo de Disponibilidad</td>
						            <td class="input"><input id="txtPeriodoDisponible" name="txtPeriodoDisponible" type="text" class="css_input_inactivo" readonly="readonly" style="width:100px; text-align: right;" runat="server" /></td>
    						    </tr>    
    	     			        <tr>
						   	        <td class="label">Clasificación del Bien</td>
							        <td class="input">
							            <input id="txtClasificacionDelBien" name="txtClasificacionDelBien" type="text" class="css_input_inactivo" readonly="readonly" style="width:160px;" runat="server" />
							        </td>	
							        <td class="label">Tipo de Bien</td>
							        <td class="input">
						                <input id="txtTipoDeBien" name="txtTipoDeBien" type="text" class="css_input_inactivo" readonly="readonly" style="width:160px;" runat="server" />
							        </td>	
							        <td class="label">Procedencia</td>
						            <td class="input">
						                <input id="txtProcedencia" name="txtProcedencia" type="text" class="css_input_inactivo" readonly="readonly" style="width:160px;" runat="server" />
						            </td>
						        </tr>	
						        <tr>    
						            <td class="label">Moneda</td>
						            <td class="input">
					                    <input id="txtMoneda" name="txtMoneda" type="text" class="css_input_inactivo" readonly="readonly" style="width:160px;" runat="server" />
						            </td>
						            <td class="label">Precio Venta</td>
						            <td class="input"><input id="txtMontoFinanciado" name="txtMontoFinanciado" type="text" class="css_input_inactivo" readonly="readonly" style="width:100px; text-align: right;" runat="server" /></td>
						            <td class="label">Ejecutivo Leasing</td>
							        <td class="input">
							            <input id="txtEjecutivoLeasing" name="txtEjecutivoLeasing" type="text" class="css_input_inactivo" readonly="readonly" style="width:160px;" runat="server" />
							        </td>
							    </tr>
							    <tr>
							        <td class="label">Clasificación de Contrato</td>
							        <td class="input">
							            <select id="cmbClasificacionContrato" name="cmbClasificacionContrato" runat="server">
                                        </select>
                                    </td>	
							        <td class="label" id="dv_lblRegistrosPublicos">
							            <div id="dv_RegistrosPublicos">
							                Registros Públicos
							            </div>
							        </td>
							        <td class="input">
							            <div id="dv_FechaRegistroPublico">
						                    <input id="chkRegistroPublico" name="chkRegistroPublico" type="checkbox" />
						                    <input id="txtFechaRegistroPublico" name="txtFechaRegistroPublico" type="text" class="css_input_inactivo" size="11" readonly="readonly" runat="server" />         
						                </div>
							       </td>   
							       <td class="label">Firmado en Notaría</td>
							        <td class="input">
							            <input id="chkFirmaNotaria" name="chkFirmaNotaria" type="checkbox" runat="server" />
							            <input id="txtFechaFirmaNotaria" name="txtFechaFirmaNotaria" type="text" class="css_input_inactivo" size="11" readonly="readonly" runat="server" />     
							       </td>  
							   </tr>
							    <tr>
								    <td class="label">Contrato Adjunto</td>
							        <td class="input" colspan="5">
							            
							            <table style="border: 0;">
							                <tr>
                                                <td>
                                                    <div id="dv_DescargarArchivoContrato"></div>
                                                </td>    
                                            </tr>
                                        </table>
                                        
							        </td>
							      </tr> 
					        </table>        					
				        </div>
				        
				        
        				<!--
        				INICIA TABS
        				-->  
        				<div id="divTabs" style="border:0; background:none;" class="dv_tabla_contenedora">
					        <ul>
					            <li><a href="#tab-6">DATOS DEL CLIENTE</a></li>
						        <li><a href="#tab-0">DATOS DEL BIEN</a></li>
						        <li><a href="#tab-1">INFORMACION REQUERIDA</a></li>
						        <li><a href="#tab-2">REPRESENTANTES A FIRMAR</a></li>
						        <li><a href="#tab-3">OTROS CONCEPTOS</a></li>
						        <li><a href="#tab-4">DATOS NOTARIALES</a></li>
						        <li id="li_adenda"><a href="#tab-5">ADENDAS</a></li>
					        </ul>
       	    	            
       	    	             <!-- 
        			         TAB :: DATOS DEL CLIENTE 
        			         !-->
       	    	             <div id="tab-6">
					            <div id="dv_datosCliente" style="border:0px;">
					                <fieldset>
						                <legend class="css_lbl_subTitulo">Datos del Cliente</legend> 
						                
						                <table id="tb_formulario" style="border:0;" cellpadding="0" cellspacing="3">
						                      <colgroup>                           
                                                <col style="width:25%;" />
                                                <col style="width:25%;" />                                                       
                                                <col style="width:25%;" />                                                       
                                                <col style="width:25%;" />        
                                              </colgroup>  
                                               
                                              <tr>
                                                  <td class="label">CU Cliente</td>
                                                  <td class="input"><input id="txtCodUnico" name="txtCodUnico" type="text" class="css_input_inactivo" size="14" readonly="readonly" runat="server" /></td>	
                                                  <td class="label">Razón social o Nombre</td>					
                                                  <td class="input"><input id="txtRazonSocial" name="txtRazonSocial" type="text" class="css_input_inactivo" readonly="readonly" runat="server" style="width: 250px;" /></td>
                                              </tr>
                                              <tr>
                                                  <td class="label">Tipo Persona</td>
							                      <td class="input"><input id="txtTipoPersona" name="txtTipoPersona" type="text" class="css_input_inactivo" readonly="readonly" runat="server" style="width: 160px;" /></td>	
							                      <td class="label" id="td_EstadoCivil">Estado Civil</td>
                                                  <td class="input">
                                                      <select id="cmbEstadoCivil" name="cmbEstadoCivil" runat="server" class="css_input">
                                                      </select>
                                                  </td>
							                  </tr>
                                              <tr>
                                                  <td class="label">Tipo de Documento</td>
                                                  <td class="input">
                                                      <input name="txtTipoDocumento" id="txtTipoDocumento" runat="server" type="text" class="css_input_inactivo" size="14" readonly="readonly" />
                                                  </td>
                                                  <td class="label">Nº de Documento</td>
                                                  <td class="input"><input id="txtNroDeDocumento" name="txtNroDeDocumento" type="text" class="css_input_inactivo" size="14" readonly="readonly" runat="server" /></td>
                                              </tr>
                                              <tr>
                                                  <td class="label">Domicilio</td>
                                                  <td class="input" colspan="2">
                                                      <textarea id="txtaDomicilioCliente" name="txtaDomicilioCliente" class="css_input_inactivo" rows="3" cols="90" runat="server" readonly="readonly"></textarea>
                                                  </td>
                                                  <td>&nbsp;</td>
                                            </tr>
                                        </table> 
                                    </fieldset>
                                    
                                    <fieldset id="fs_DatosConyugue">
                                       <legend  class="css_lbl_subTitulo">Datos del Cónyuge</legend>
                                          
				                       <table id="tb_formulario" style="border:0;" cellpadding="0" cellspacing="3">
                                           <colgroup>
                                              <col style="width:25%;" />
                                              <col style="width:25%;" />
                                              <col style="width:25%;" />
                                              <col style="width:25%;" />
                                           </colgroup>   
                                             
                                              <tr>
                                                  <td class="label">Nombre del Cónyuge</td>
                                                  <td class="input"><input id="txtNombreConyuge" name="txtNombreConyuge" type="text" class="css_input" runat="server" style="width: 350px;" /></td>                             
                                              </tr>
                                              <tr>
                                                  <td class="label">Tipo de Doc.</td>
                                                  <td class="input">
                                                      <select name="cmbTipoDocumentoConyuge" id="cmbTipoDocumentoConyuge" runat="server">
                                                      </select>
                                                  </td>
                                                  <td class="label">Nº de Doc.</td>
                                                  <td class="input"><input id="txtnumerodocumento" name="txtnumerodocumento" type="text" class="css_input" size="14" runat="server" />									
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td class="label">Documento de Separación</td>
                                                  <td colspan="3" class="input">
                                                      
                                                      <table style="border: 0;">
                                                          <tr>
							                                  <td>
                                                                 <img title="Adjuntar correo" id="imgAdjuntarArchivoDocumentoSeparacion" style="cursor: pointer;cursor: hand;" alt="" src="../Util/images/ico_acc_adjuntarMini.gif" />
                                                              </td>
                                                            <td>
                                                                <div id="dv_AdjuntarArchivoDocumentoSeparacion" style="border: 0px;"></div>
                                                            </td>    
                                                        </tr>
                                                    </table>
                                                    
                                                  </td>
                                              </tr>
                                          </table>
                                      </fieldset>
                                              
                                    <fieldset >
                                       <legend  class="css_lbl_subTitulo">Datos de Representantes a Firmar</legend> 
                                        <table id="tb_formulario" cellpadding="0" cellspacing="0" style="border: 0;">
                                          <tr>
                                              <td>
                                                    
                                                  <table id="jqGrid_lista_H"><tr><td/></tr></table> 
                                                  <div id="jqGrid_pager_H"></div>
                                                            
                                              </td>  
                                          </tr>
                                      </table>
                                  </fieldset>

    				            </div>	
       	    	             </div>
       	    	             
					         <!-- 
        			         TAB :: DATOS DEL BIEN
        			         !-->	
					         <div id="tab-0">
				                <!-- Datos Inmueble -->
				                <div id="dvDatosBien" style="border:0;">
				                
				                    <fieldset>
					                    <legend  class="css_lbl_subTitulo">Datos del Bien</legend>
                       
                                        <table>                                   
                                            <tr>
                                                <td>
                                                    
                                                    <table id="jqGrid_lista_A"><tr><td/></tr></table>
						                            <div id="jqGrid_pager_A"></div>
						                            
                                                </td>                                                    
                                            </tr>
                                        </table>
                                    </fieldset>
                                 </div>   
                                  
                                <!-- Datos Maquinaria -->
                                <div id="dvDatosMaquinaria" style="border:0px;">                            
                                    <fieldset>
					                    <legend  class="css_lbl_subTitulo">Datos del Bien</legend>
	                                    
                                        <table>
                                             <tr>
                                                <td>
                                                
                                                    <table id="jqGrid_lista_B"><tr><td/></tr></table>
						                            <div id="jqGrid_pager_B"></div>
						                            
                                                </td>
                                             </tr>						    
                                        </table>
                                    </fieldset>
                                </div>
                                
				                <!-- Datos Otros -->
                                <div id="dvDatosOtros" style="border:0px;">
				                    <fieldset>
					                    <legend  class="css_lbl_subTitulo">Datos del Bien</legend>
					                    
                                        <table>                                   
                                           <tr>
                                                <td>
                                                    
                                                    <table id="jqGrid_lista_J"><tr><td/></tr></table>
						                            <div id="jqGrid_pager_J"></div>
						                            
                                                </td>                                                    
                                           </tr>
                                        </table>
                                    </fieldset>
                                 </div> 
                                 
                               <!-- Proveedores -->  
                               <fieldset>
					                <legend class="css_lbl_subTitulo">Proveedores</legend>   
					                      						            
						            <table id="jqGrid_lista_F"><tr><td/></tr></table>
						            <div id="jqGrid_pager_F"></div>
					           </fieldset>
       	    	             </div>
       	    	            
       	    	             <!-- 
        			         TAB :: CONDICIONES ADICIONALES
        			         !-->	
        			         <div id="tab-1">
					            <div class="dv_tabla_contenedora">
		                            <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
		                                <tr>
    		                                <td><strong>CONDICIONES ADICIONALES</strong></td>   	                                
		                                </tr>
                                        <tr>
                                            <td> 
                                                <table style="border:0;">
                                                    <tr>
    	                                                <td>
    	                                             		
                                                            <table id="jqGrid_lista_E"><tr><td/></tr></table>
                                                            <div id="jqGrid_pager_E"></div>
                                                        
                                                        </td>                                      
                                                    </tr>				
		                                        </table>
                                            </td>                                        
                                        </tr>				
		                            </table>
		                         </div>  
		                         <div class="dv_tabla_contenedora">    
		                            <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
		                                <tr>
		                                    <td><strong>DOCUMENTOS</strong></td>  
		                                </tr>
		                                <tr>
                                            <td> 
                                                <table style="border:0;">
    			                                  <tr>
    			                                      <td>   		
    			                                      				
                                                        <table id="jqGrid_lista_K"><tr><td/></tr></table>
                                                        <div id="jqGrid_pager_K"></div>
                                                        
                                                     </td>                                        
                                                    </tr>				
		                                        </table>
                                            </td>                                        
                                        </tr>				
		                            </table>
			                    </div>    
       	    	            </div>
       	    	            
        			         <!-- 
        			         TAB :: REPRESENTANTES A FIRMAR
        			         !-->
        				     <div id="tab-2">
	                           <div class="dv_tabla_contenedora">
	                           
				                 <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
					                <tr>
					                    <td>
					                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
			                                    <tr>
				                                    <td class="titulo css_lbl_tituloContenido">Representantes Interbank</td>
			                                    </tr>
		                                    </table>    
		                                </td>     
		                            </tr>
		                            <tr>
					                    <td>
					                        <table style="border: 0;">
			                                  <tr>
			                                      <td>
			                                      
		                                            <table id="jqGrid_lista_C"><tr><td/></tr></table>
                                                    <div id="jqGrid_pager_C"></div>
                                                    
                                                </td>
					                         </tr>
				                          </table>
					                    </td>
					                 </tr>
				                  </table>
        				      	</div>
       	    	            </div>
        				
        				     <!-- 
        			         TAB :: OTROS CONCEPTOS
        			         !-->
        				     <div id="tab-3">
					            <div class="dv_tabla_contenedora">
					                <fieldset>
						                <legend class="css_lbl_subTitulo">Tasas y Comisiones</legend>     
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">  
                                            <colgroup>
                                                <col style="width:16.66%;" />
                                                <col style="width:16.66%;" />
                                                <col style="width:16.66%;" />
                                                <col style="width:16.66%;" />
                                                <col style="width:16.66%;" />
                                                <col style="width:16.66%;" />
                                            </colgroup>
                                         
                                            <tr>
	                                            <td class="label">T.E.A. %</td>
	                                            <td class="input"><input id="txtTea" name="txtTea" type="text" class="css_input_inactivo" size="10" runat="server" style="text-align: right;" readonly="readonly" /></td>		                            
	                                            <td class="label">Precuota %</td>
	                                            <td class="input"><input id="txtprecuota" name="txtprecuota" type="text" class="css_input_inactivo" size="10" runat="server" style="text-align: right;" readonly="readonly" /></td>
	                                            <td class="label">Opción de Compra</td>
	                                            <td class="input"><input id="txtOpcionCompra" name="txtOpcionCompra" type="text" class="css_input_inactivo" style="width: 160px; text-align:right;" runat="server" readonly="readonly" /></td>		                            
	                                       </tr>
	                                       <tr>
	                                            <td class="label">Comisión de Activación</td>
	                                            <td class="input"><input id="txtComisionActivacion" name="txtComisionActivacion" type="text" class="css_input_inactivo" size="10" runat="server" style="text-align: right;" readonly="readonly" /></td>		                            
	                                            <td class="label">Comisión de Estructuración</td>
	                                            <td class="input"><input id="txtComisionEstructuracion" name="txtComisionEstructuracion"  type="text" class="css_input_inactivo" size="10" runat="server" style="text-align: right;" readonly="readonly" /></td>		                            
                                                <td class="label">Otras Comisiones</td>
                                                <td class="input">
                                                    <textarea id="txtaOtrasComisiones" name="txtaOtrasComisiones" cols="20" rows="2" style="width:350px;" runat="server" class="css_input_inactivo" readonly="readonly"></textarea>
	                                            </td>		                            
                                            </tr>	
		                                </table>
			                           </fieldset>    
    					            
					                    <fieldset>
						                    <legend class="css_lbl_subTitulo">Penalidades</legend>     
                                            <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                                <colgroup>                           
                                                    <col style="width:25%;" />
                                                    <col style="width:25%;" />
                                                    <col style="width:25%;" />
                                                    <col style="width:25%;" />
                                                </colgroup>
                                                 
  	                                            <tr>
		                                            <td class="label">% Del Importe Pendiente de Pago, por Día de Atraso</td>
		                                            <td class="input"><input id="txtImporteAtrasoPorc" name="txtImporteAtrasoPorc" type="text" class="css_input" size="10" runat="server" style="text-align: right;" /></td>		                            
    		                                        <td class="label">Otros</td>
		                                            <td class="input">
		                                                <textarea id="txtaOtrasPenalidades" name="txtaOtrasPenalidades" cols="20" rows="2" style="width:350px;" runat="server"></textarea>
		                                            </td>
		                                        </tr>     
		                                        <tr>     
		                                            <td class="label">Días desde el Vencimiento a la Fecha de Pago</td>
		                                            <td class="input"><input id="txtdiasVencimiento" name="txtdiasVencimiento" type="text" class="css_input" size="10" runat="server" style="text-align: right;" />&nbsp;&nbsp;días</td>
		                                            <td class="label">Adjuntar Correo</td>
		                                            <td class="input">
	                                                   <table>
	                                                       <tr>
	                                                          <td>
		                                                           <img src="../Util/images/ico_acc_adjuntarMini.gif" id="img_ArchivoOtroConcepto" alt="" title="Adjuntar correo" style="cursor: pointer;cursor: hand;" />
		                                                       </td>
		                                                       <td>
                                                                   <div id="dv_ArchivoOtroConcepto" style="border:0;"></div>
                                                               </td>
                                                          </tr>
                                                      </table>
		                                            </td>
		                                        </tr>    
		                                        <tr>        
		                                            <td class="label">% De la cuota</td>
		                                            <td class="input" colspan="3"><input id="txtPorcentajeCuota" name="txtPorcentajeCuota" runat="server" type="text" class="css_input" size="10" style="text-align: right;" /></td>
	                                            </tr>	
			                                </table>
			                           </fieldset>     
			                   
			                    </div>    
       	    	            </div>
       	    	            
       	    	             <!-- 
        			         TAB :: DATOS NOTARIALES
        			         !-->	
       	    	             <div id="tab-4">
					            <div id="dv_datosNotariales" style="border:0;">
					                <fieldset>
					                
    			                       <table style="border:0;">
		                                  <tr>
                                              <td>
                                                <table id="jqGrid_lista_G"><tr><td/></tr></table>      
                                                <div id="jqGrid_pager_G"></div>
                                              </td>
		                                  </tr>
		                              </table>  
                                                           
					                </fieldset>       					
				                </div>
       	    	            </div>
       	    	            
       	    	             <!-- 
        			         TAB :: ADENDAS
        			         !-->
       	    	             <div id="tab-5" >
					            <div id="dv_adendas" style="border:0;">
					                <fieldset>
                                     
                                         <table style="border: 0;">
                                          <tr>
                                              <td>
                                                  
                                                <table id="jqGrid_lista_I"><tr><td/></tr></table>  
                                                <div id="jqGrid_pager_I"></div>
                                                
                                              </td>
                                          </tr>
                                        </table>     
		                           </fieldset>
       	    	                 </div>
       	    	             </div>
       	    	         
       	    	         </div >    
			        </td>
		        </tr>
	        </table>   
	    </div>
    	
    </div>
    
    <div>
        <input id="hdnCodigoNotarial" type="hidden" />
        <input id="hddCodigoCotizacion" type="hidden" runat="server" />
        <input id="hddCorreocontacto" type="hidden" runat="server" />
        <input id="hdnCodigoAdenda" type="hidden" runat="server" />
        <input id="hddCodProductoFinancieroActivo" type="hidden" runat="server" />
        <input id="hddCodMoneda" type="hidden" runat="server" />
        <input id="hddTipoDocumentoConyuge" type="hidden" runat="server" />
        <input id="hddCodigoTipoPersona" type="hidden" runat="server" />
        <input id="hddTipoDocumento" type="hidden" runat="server" />
        <input id="hddCodigoEstadoCivil" type="hidden" runat="server" />
        <input id="hddEstadoCivil" type="hidden" runat="server" />
        <input id="hddCodigoContrato" type="hidden" runat="server" />
        <input id="hddTipoRubroFinanciamiento" type="hidden" runat="server" />
        <input id="hddCodigoClasificacionContrato" type="hidden" runat="server" />
        <input id="hddCodigoTipoBien" type="hidden" runat="server" />
        <input id="hddSecFinanciamiento" type="hidden" runat="server" />
        <input id="hddRowId" type="hidden" runat="server" />
        <input id="hddCodProveedor" type="hidden" runat="server" />
        <input id="hddCodigoEstadoBien" type="hidden" runat="server" />
        <input id="hddCodigoEstadoContrato" type="hidden" runat="server" />
        <input id="hddFechaFirmaNotaria" type="hidden" runat="server" />
        <input id="hddClasifContratoSeleccion" type="hidden" runat="server" />
        <!-- Datos del bien -->
        <!-- Detecta cambios en el contenido de los controles, antes de salir de la actual ventana. -->
        <input id="hddCambiosSinGuardar" type="hidden" runat="server" />
        <!-- Indica si alguno de los datos del contrato o de otros documentos han cambiado y requieren volver a generar el contrato. 
             El cambio se registra a través de la base de datos -->
        <input id="hddFlagModificado" type="hidden" runat="server" />
        
        <input id="hddMensajeCorreo" type="hidden" runat="server" />
        <input id="hddFechaActual" type="hidden" runat="server" />
        
        <!-- Datos del bien -->
        <input id="hddUso" type="hidden" runat="server" />
        <input id="hddUbicacion" type="hidden" runat="server" />
      
        <!-- Contrato Valida Modificaciones -->
        <input id="hddAdjuntarArchivo" type="hidden" runat="server" />
        <input id="hddValidaModificacion" type="hidden" runat="server" />
        <input id="hddGeneraContrato_Adjunto" type="hidden" runat="server" />
        
        <!-- Datos del conyugue -->
        <input id="hddAdjuntarArchivoDocumentoSeparacion" type="hidden" runat="server" />
        <!-- Otros conceptos -->
        <input id="hddAdjuntarArchivoOtroConcepto" type="hidden" runat="server" />
        <!-- Adenda - nuevo -->
       <input id="hddAdjuntarArchivoNotarialNuevo" type="hidden" runat="server" />
        <!-- Adenda - editar -->
        <input id="hddAdjuntarArchivoNotarialEditar" type="hidden" runat="server" />
        <!-- Texto predefinido -->
        <input id="hddTextoPredefinido" type="hidden" runat="server" />
        
    </div>

    </form>
</body>
</html>
