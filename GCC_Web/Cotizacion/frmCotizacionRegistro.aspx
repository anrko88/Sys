<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCotizacionRegistro.aspx.vb" Inherits="Cotizacion_frmCotizacionRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
	<script type="text/javascript" src="../Util/js/jquery/jquery.dateFormat-1.0.js"></script>
	<script type="text/javascript" src="../Util/js/jquery/json2.js" ></script>
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.grilla.js"> </script>	
	
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmCotizacionRegistro.aspx.js"> </script>
	
</head>
<script language="javascript" type="text/javascript">
    document.onkeydown = function(evt) { return (evt ? evt.which : event.keyCode) != 13; }
    document.onkeypress = function(evt) { if ((evt ? evt.which : event.keyCode) == 39) { event.keyCode = 96; return event.keyCode; } }
</script>
<body>    
<form id="frmCotizacionRegistro" runat="server">

    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        
        <!-- BOTONES DE CABEZERA-->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
		     <tr>
			    <td class="icono">
				    <img alt="" src="../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono"/>		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo">Cotización y Negociación</div>
				    <div class="css_lbl_titulo">Cotización :: <label id="lblOperacion" runat="server"></label></div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones" >
			    
			        <div id="dv_botonCancelar" class="dv_img_boton">
					    <a href="javascript:fn_cancelar();">
						    <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
						    Volver
					    </a>
				    </div>
                    
                    <div id="dv_botonEnviar" class="dv_img_boton" style="width:82px;">
					    <a href="javascript:fn_grabar('1','1');">
						    <img alt="" src="../Util/images/ico_acc_grabarEnviar.gif" border="0" /><br />
						    Guardar y Enviar
					    </a>
				    </div>
				        				
				    <div id="dv_botonGrabar" class="dv_img_boton">
					    <a href="javascript:fn_grabar('0','0');">
						    <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
						    Guardar
					    </a>
				    </div>
				       
				    <div class="dv_img_boton_separador" id="dv_SeparadorGuardar">
				        :
				    </div>
				    
				    <div id="dv_botonGenerar" class="dv_img_boton">
					    <a href="javascript:fn_generaCronograma();" title="Generar Cronograma">
						    <img alt="" src="../Util/images/ico_acc_cronograma.gif" border="0"/><br/>
						    Generar
					    </a>
				    </div>
				    
				    <div id="dvGstSupervisor" style="display:none;">
    				    
				        <div class="dv_img_boton_separador">
				            :
				        </div>
    				        
				        <div id="dv_botonDevolver" class="dv_img_boton">
					        <a href="javascript:fn_grabar('2','2');">
						        <img alt="" src="../Util/images/ico_acc_lstRemove.gif" border="0" /><br />
						        Devolver
					        </a>
				        </div>
    				    
				        <div id="dv_botonAprobar" class="dv_img_boton">
					        <a href="javascript:fn_grabar('3','3');">
						        <img alt="" src="../Util/images/ico_acc_grabarEnviar.gif" border="0" /><br />
						        Aprobar
					        </a>
				        </div>
				    
                    </div>
                        
				    <div id="dv_OpcMenu">    
				        <div class="dv_img_boton_separador">
			                :
			            </div>				        
			            <div id="Div1" class="dv_img_boton" style="width:70px;">
				            <a href="javascript:fn_abreSeguimiento();">
					            <img alt="" src="../Util/images/ico_seguimiento.gif" border="0" /><br />
					            Seguimiento
				            </a>
			            </div>    				    
			            <div id="Div2" class="dv_img_boton" style="width:70px;">
				            <a href="javascript:fn_abreVersiones();">
					            <img alt="" src="../Util/images/ico_version.gif" border="0" /><br />
					            Versiones
				            </a>
			            </div>                        					    			
    			    </div>	
    			    
			    </td>
		    </tr>
        </table>	
        <!-- FIN BOTONES DE CABEZERA-->
        
	    <div id="dv_contenedor" class="css_scrollPane" >
	        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
		            <tr><td class="lineas"></td></tr>
		            <tr>
			            <td class="cuerpo">
				            <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
					            <tr>
						            <td class="titulo css_lbl_tituloContenido">Datos de la Cotización</td>
						            <td class="botones">							
							            <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						            </td>
					            </tr>
				            </table>
    				        
    				        <!-- Atributos Ocultos -->
    				        <input type="hidden" id="hddCodigoCotizacion" name="hddCodigoCotizacion" value="" runat="server" />
    				        <input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="NUEVO" runat="server" />
    				        <input type="hidden" id="hddFechaActual" name="hddFechaActual" value="" runat="server" />
    				        <input type="hidden" id="hddIGV" name="hddIGV" value="18" runat="server" />
    				        <input type="hidden" id="hddIGVUtilizado" name="hddIGVUtilizado" value="18" runat="server" />
    				        <input type="hidden" id="hddCodigoContacto" name="hddCodigoContacto" value="" runat="server" />
    				        <input type="hidden" id="hddCodigoDocumento" name="hddCodigoDocumento" value="" runat="server" />
    				        <input type="hidden" id="hddVersionCotizacion" name="hddVersionCotizacion" value="" runat="server" />
    				        <input type="hidden" id="hddPathArchivo" name="hddPathArchivo" value="" runat="server" />
    				        <input type="hidden" id="hddValidaDataCronograma" name="hddValidaDataCronograma" value="" runat="server" />
    				        <input type="hidden" id="hddCodUnico" name="hddCodUnico" value="" runat="server" />
    				        
    				        <input type="hidden" id="hddSpreadBanca" name="hddSpreadBanca" value="1" runat="server" />    				        
    				        <input type="hidden" id="hddComisionActivacion" name="hddComisionActivacion" value="" runat="server" /> 
    				        
    				        <input type="hidden" id="hddIniTipoCronograma" name="hddIniTipoCronograma" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniNroCuotas" name="hddIniNroCuotas" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniPeriodicidad" name="hddIniPeriodicidad" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniFrecuenciaPago" name="hddIniFrecuenciaPago" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniTEA" name="hddIniTEA" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniTipoBienSeguro" name="hddIniTipoBienSeguro" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniPrecioVenta" name="hddIniPrecioVenta" value="" runat="server" />    				        
    				        <input type="hidden" id="hddIniCuotaIni" name="hddIniCuotaIni" value="" runat="server" />
    				        <input type="hidden" id="hddIniCuotaIniPorc" name="hddIniCuotaIniPorc" value="" runat="server" />
    				        <input type="hidden" id="hddIniIGVPorc" name="hddIniIGVPorc" value="" runat="server" />
    				        <input type="hidden" id="hddIniPlazoGracia" name="hddIniPlazoGracia" value="" runat="server" />
    				        <input type="hidden" id="hddIniTipoGracia" name="hddIniTipoGracia" value="" runat="server" />
    				        <input type="hidden" id="hddIniFecVenc" name="hddIniFecVenc" value="" runat="server" />
    				        <input type="hidden" id="hddIniFecMaxAct" name="hddIniFecMaxAct" value="" runat="server" />
    				        <input type="hidden" id="hddIniImportePrimaSeguroBien" name="hddIniImportePrimaSeguroBien" value="" runat="server" />
    				        <input type="hidden" id="hddIniNumCuotasfinanciadas" name="hddIniNumCuotasfinanciadas" value="" runat="server" />
    				         
    				        <input type="hidden" id="hddIniSupTEA" name="hddIniSupTEA" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniSupPreCuotaPorc" name="hddIniSupPreCuotaPorc" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniSupSpread" name="hddIniSupSpread" value="" runat="server" /> 
    				        <input type="hidden" id="hddIniComisionActivacionMonto" name="hddIniComisionActivacionMonto" value="" runat="server" /> 
    				           		
    				        <input type="hidden" id="hddMontoTemporal" name="hddMontoTemporal" value="" runat="server" />
    				        <input type="button" name="btnListaDoc" id="btnListaDoc" onclick="javascript:fn_cargaGrillaDocumento();" style="display:none;"/>
    				        <asp:Button ID="btnGeneraCronograma" runat="server" Style="display:none;" Text="GeneraCronograma" />
    				        
    				        <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" value="" runat="server" />
    				        <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" value="" runat="server" />
    				        <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" value="" runat="server" />
    				        <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" value="" runat="server" />
    				        <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" value="" runat="server" />
    				        
    				        <input type="hidden" id="hddFlagRetorno" name="hddFlagRetorno" value="" runat="server" />
    				        <input type="hidden" id="hddDireccionCliente" name="hddDireccionCliente" value="" runat="server" />
    				            		
                            <input type="hidden" name="hddError" id="hddError" value="" runat="server" />
                            
                            <input type="hidden" id="hddPorcentajeComision" name="hddPorcentajeComision" value="" runat="server" />
                            
                            <!--IBK - RPH -->
                            <input type="hidden" name="hddAdjuntarArchivo" id="hddAdjuntarArchivo" value="" runat="server" />
                            <input type="hidden" name="hddGeneraCronograma" id="hddGeneraCronograma" value="" runat="server" />                            
                            <input type="hidden" name="hddValidaEnviar" id="hddValidaEnviar" value="" runat="server" />
                            <input type="hidden" name="hidPerfil" id="hidPerfil" value="" runat="server" />
                            <!--Fin -->                                   				            				        
        			        <!-- ******************************************************************************************** -->	
        			        <!-- Inicia de Cabecera -->
        			        <!-- ******************************************************************************************** -->	        			    
				            <div id="dv_datos" class="dv_tabla_contenedora">
        					        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%">
        					            <colgroup>    
        					             <!--                       
                                            <col style="width:16.5%" />
                                            <col style="width:16.5%" />
                                            <col style="width:16.5%"/>
                                            <col style="width:16.5%" />
                                            <col style="width:16.5%"/>
                                            <col style="width:16.5%" />
                                             -->
                                            <col style="width:15.5%" />
                                            <col style="width:15.5%" />
                                            <col style="width:15.5%"/>
                                            <col style="width:20.5%" />
                                            <col style="width:12.5%"/>
                                            <col style="width:15.5%" />
                                            <!-- Fin IBK -->    
                                        </colgroup>
            					            					        
					                    <tr>
					                        <td class="label">Nro. Cotización</td>
							                <td class="input" >
							                    <input id="txtNumeroCotizacion"  name="txtNumeroCotizacion" type="text" class="css_input_inactivo" value="" size="15" runat="server" />							            
							                </td>
							                <td class="label"  >Estado</td>
							                <td class="input"  >
							                    <select id="cmbEstado" name="cmbEstado" runat="server">
							                        <option>- Seleccionar -</option>
    							                </select>
	    						            </td>
    					                    
					                        <td class="label">Generar Carta</td>
					                       <td class="input">
					                            <input id="chkGeneraCarta" name="chkGeneraCarta" type="checkbox" runat="server" onclick="javascript:fn_util_realizaCheck('chkGeneraCarta', 'hddGeneraCarta');"/>
					                            <input id="hddGeneraCarta" name="hddGeneraCarta" type="hidden" value="0" runat="server"/>
					                            <label id="lblFechacarta" runat="server"></label>
					                       </td>
    			    		               
					                    </tr>
						                <tr>
							                <td class="label"  >CU Cliente</td>
							                <td class="input"   >
							                    <input id="chkValidaCliente" name="chkValidaCliente" type="checkbox" runat="server"/>
					                            <input id="hddValidaCliente" name="hddValidaCliente" type="hidden" value="0" runat="server"/>
					                            <input id="hddCodSuprestatario" name="hddCodSuprestatario" type="hidden" value="" runat="server"/>
							                    <input id="txtCUCliente" name="txtCUCliente" type="text"  class="css_input" value="" MaxLength="14" size="14" runat="server" />
							                    <img id="imgBsqClienteRM" alt="" src="../Util/images/ico_buscar.jpg" style="cursor:pointer; vertical-align:middle;" runat="server" />
							                </td>
							                <td class="label"  >Razón Social o Nombre</td>
							                <td class="input"  >
							                    <input id="txtNombreCliente" name="txtNombreCliente" type="text" class="css_input" value="" size="30" runat="server" />
							                    <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                                    onclick="javascript:fn_abreBusquedaCliente();" />
							                </td>
							                <td class="label"  >Tipo Persona</td>
							                <td class="input"  >
							                        <select id="cmbTipoPersona" name="cmbTipoPersona" runat="server">
					                                    <option value="0">- Seleccionar</option>
                                                    </select>
							                </td>
    							            
						                </tr>
						                <tr>
						                    <td class="label">Tipo de Documento</td>
							                <td class="input">
							                    <select id="cmbTipoDocumento" name="cmbTipoDocumento" runat="server">
							                        <option value="0">- Seleccionar -</option>    							                   
							                    </select>
							                </td>
    						                
						                     <td class="label"  >Nro. Documento</td>
							                <td class="input"  >
							                    <input id="txtNumeroDocumento" name="txtNumeroDocumento" type="text" class="css_input" value="" size="30" runat="server" />
							                </td>
    							            
							                <td class="label" >
                                                <input id="chkLinea" name="chkLinea" type="checkbox" onclick="javascript:fn_util_realizaCheck('chkLinea','hddLinea');" runat="server"/>
                                                <input id="hddValidaLinea" name="hddValidaLinea" type="hidden" value="0" runat="server"/>
					                            Nº Linea
					                        </td>
					                        <td class="input" >
					                           <input id="hddTasaLinea" name="hddTasaLinea" type="hidden" value="0"/>
					                           <select id="cmbLinea" name="cmbLinea" class="" runat="server">					                               
						                       </select>
						                       <img id="imgBusqLinea" alt="" src="../Util/images/ico_buscar.jpg" style="cursor:pointer; vertical-align:middle;" onclick="javascript:fn_DetalleLineas();" />
			    		                    </td>
    							            
						                </tr>
						                <tr>
						                    <td class="label"  >Ejecutivo de la Banca</td>
						                    <td class="input"  >
						                        <select id="cmbEjecutivoBanca" class="" runat="server" visible="false">
							                        <option value="0">- Seleccionar -</option>
							                    </select>			
							                    <input id="txtEjecutivoBanca" name="txtEjecutivoBanca" type="text" class="css_input_inactivo" value="" size="30" runat="server" />
						                    </td>
						                    <td class="label"  >Banca en atención</td>
							                <td class="input"  >
							                    <select id="cmbBancaAtencion" name="cmbBancaAtencion" runat="server">
							                        <option value="0">- Seleccionar -</option>
							                    </select>
							                </td>
    						               
						                    <td class="label"  >Zonal</td>
						                    <td class="input"  >
						                        <select id="cmbZonal" class="" runat="server" visible="false">
					                                <option value="0">- Seleccionar</option>
						                       </select>    
						                       <input id="txtZonal" name="txtZonal" type="text" class="css_input_inactivo" value="" size="30" runat="server" />						                
						                    </td>
						                </tr>
						                <tr>
						                    <td class="label"  >Contacto</td>
							                <td class="input"  ><input id="txtContacto" type="text" class="css_input" value="" size="30" maxlength="50" runat="server"/></td>
    							          	
    						                <td class="label"  >Correo</td>
							                <td class="input"  ><input id="txtCorreo" type="text" class="css_input" value="" size="30" maxlength="50" runat="server"/></td>
    							            						                
					                       <td class="label"  >Ejecutivo Leasing</td>
						                    <td class="input"  >
						                        <select id="cmbEjecutivoLeasing" name="cmbEjecutivoLeasing" runat="server" runat="server" >
					                               <option value="0">- Seleccionar</option>
						                       </select>
						                    </td>			                   
				    		            </tr>
					              </table>
				           </div>        			    
        			        <!-- Fin de Cabecera -->
            			    
            			    
            			    
        			        <!-- ******************************************************************************************** -->	
        			        <!-- Inicia Tabs -->	
        			        <!-- ******************************************************************************************** -->	        			    
			                <div id="divTabs" style="border:0px; background:none;" class="dv_tabla_contenedora" >
					            <ul>
						            <li><a href="#tab-0">DATOS GENERALES</a></li>
						            <li><a href="#tab-1">OPCIONES</a></li>
						            <li id="tab2-tab"><a href="#tab-2">DOCUMENTOS Y COMENTARIOS</a></li>
						            <li id="tab3-tab"><a href="#tab-3">CRONOGRAMA</a></li>
					            </ul>
    					        
					            <!-- **************** -->	
        			            <!-- TAB :: GENERAL   -->	 
        			            <!-- **************** -->	
					            <div id="tab-0" >
					                <fieldset>
					                    <legend class="css_lbl_subTitulo">Datos de la Cotización</legend>
    					                
       						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%">
       						                 <colgroup>                           
                                                <col style="width:16.66%" />
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                            </colgroup>
       						                <tr>  
                                                <td class="label" >Tipo de Contrato</td>
    						                    <td class="input" >
    						                            <input type="hidden" id="hddProdFinanActivo" name="hddProdFinanActivo" />
    						                            <input type="hidden" id="hddProdFinanPasivo" name="hddProdFinanPasivo" />
						                                <!-- Inicio IBK - AAE -->						                                
						                                <select id="cmbTipoContrato" name="cmbTipoContrato" runat="server" onchange="javascript:fn_EstadoBien(this.value,'0');">						                                
						                                    <option value="0">-Seleccionar-</option>
						                                </select>
    						                            <!-- Fin IBK -->    						                            
						                                <select id="cmbSubTipoContrato" name="cmbSubTipoContrato" runat="server" >
						                                    <option value="0">- Seleccionar</option>    						                                 
						                                </select>
						                        </td>
						                        <td class="label" >Moneda</td>
    						                     <td class="input" >
						                            <select id="cmbMoneda" name="cmbMoneda" runat="server">
        						                        <option value="0">- Seleccionar -</option>    
						                            </select>
    						                     </td>      						                 
                                                 <td class="label" >Procedencia</td>
                                                 <td class="input" >
                                                      <select id="cmbprocedencia" name="cmbprocedencia" runat="server" >
                                                          <option value="0">- Seleccionar -</option> 
                                                      </select>
                                                </td>                                            
                                            </tr>
                                            <tr>
                                                <td class="label" >Clasificación del Bien</td>
                                                <td class="input" >
                                                        <select id="cmbClasificacionBien" name="cmbClasificacionBien" runat="server">
                                                            <option value="0"> - Seleccionar -</option>
                                                        </select>
                                                 </td>
                                                 <td class="label" >Tipo de Bien</td>
    						                     <td class="input" >
    						                           <select id="cmbTipoBien" name="cmbTipoBien" runat="server">
    						                                 <option value="0"> - Seleccionar -</option>       
  						                               </select>
    						                     </td>
    						                     <td class="label" >Estado del bien</td>
						                         <td class="input" >
						                                 <!-- Inicio IBK - AAE-->						                                 
						                                 <select id="cmbEstadoBien" name="cmbTipoBien" runat="server" onchange="javascript:fn_EstadoBien(this.value,'1');" >						                                 
						                                     <option value="0"> - Seleccionar -</option>
					                                     </select>
					                                     <!-- Fin IBK -->
						                         </td>    						                 
                                            </tr>                                                 
                                            <tr> 
                                                 <td class="label" >Precio Venta</td>  
						                         <td class="input" >
						                               <input id="txtPrecioVenta" name="txtPrecioVenta" type="text" class="css_input" value="" size="15" runat="server"/>
						                         </td>   						                     
						                         <td class="label" >IGV</td>
    						                     <td class="input" >
    						                            <input id="txtPorcIGV" name="txtPorcIGV" type="text" class="css_input" value="" size="4" runat="server" />%
    						                            -
    						                            <input id="txtMontoIGV" name="txtMontoIGV" type="text" class="css_input" value="" size="10" runat="server" />    						                            
    						                     </td>	
    						                     <td class="label" >Valor Venta</td>
    						                     <td class="input" >
    						                            <input id="txtValorVenta" name="txtValorVenta" type="text" class="css_input" value="" size="15" runat="server" />
    						                     </td>					                     
						                     </tr>
						                     <tr> 
						                        <td class="label" >
						                            <input type="hidden" name="hddTipoCuota" id="hddTipoCuota" value="0" runat="server" />
						                            <input id="rdbTipoCuotaMonto" name="rdbTipoCuota" type="radio" runat="server" onclick="javacript:fn_validaTipoCuota('1');"/>
						                            <span >Cuota Inicial </span>
						                        </td>
						                        <td class="input" >
						                            <input id="txtCuotaInicial" name="txtCuotaInicial" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td> 
						                        <td class="label" >
						                            <input id="rdbTipoCuotaPorc" name="rdbTipoCuota" type="radio" runat="server" onclick="javacript:fn_validaTipoCuota('0');"/>
						                            <span class="label">Cuota Inicial %</span>
						                        </td>
    						                    <td class="input" >
    						                           <input id="txtCuotaInicialPorc" name="txtCuotaInicialPorc" type="text" class="css_input" value="" size="7" runat="server"/>
    						                    </td>
    						                    <td class="label" >Riesgo Neto</td>
						                        <td class="input" >
						                               <input id="txtRiesgoNeto" name="txtRiesgoNeto" type="text" class="" value="" size="15" runat="server"/>
						                        </td>
    						                 </tr>
						                </table>
    						            
						            </fieldset>
						            <fieldset>
						                <legend  class="css_lbl_subTitulo">Datos Cronograma</legend>
    						            
       						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:100%">
       						                <colgroup>                           
                                                <col style="width:16.66%" />
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                            </colgroup>
					                        <tr>
				                                <td class="label" >Tipo de Cronograma</td>
			                                    <td class="input" >
			                                        <select id="cmbTipoCronograma" name="cmbTipoCronograma" runat="server" >
			                                            <option value="0">- Seleccionar -</option>
			                                        </select>
			                                    </td>
			                                    <td class="label" >Nro. Cuotas</td>
			                                    <td class="input" >
			                                            <input id="txtNroCuotas" name="txtNroCuotas" type="text" class="css_input" value="" size="11" runat="server"/>
			                                    </td>
			                                    <td class="label">Periodicidad</td>
			                                    <td class="input">
		                                                <select id="cmbPeriodicidad" name="cmbPeriodicidad"  runat="server">
		                                                    <option value="0">- Seleccionar -</option>
		                                                </select>
			                                    </td>
				                            </tr>
				                            <tr> 
				                                 <td class="label" >Frecuencia de Pago</td>
    						                     <td class="input" >
    						                            <select id="cmbFrecuenciaPago" name="cmbFrecuenciaPago" runat="server">
    						                                <option value="0">-Seleccionar-</option>        						                     
					                                    </select>
    						                     </td>
    						                     <td class="label">Plazo Gracia</td>
			                                     <td class="input"><input id="txtPlazoGracia" name="txtPlazoGracia" type="text" class="css_input" value="" size="3" runat="server"/>&nbsp;cuotas</td>
			                                     <td class="label">Tipo de Gracia</td>
			                                     <td class="input">
		                                            <select id="cmbTipoGracia" name="cmbTipoGracia"  runat="server">
		                                                <option value="0">- Seleccionar -</option>    		                                            
		                                            </select>
			                                     </td>                               
					                        </tr>
					                        <tr>	
					                             <td class="label" >Fecha Activación</td>
		                                         <td class="input">
		                                             <input id="txtFechaMaxActivacion" name="txtFechaMaxActivacion" type="text" class="css_input" value="" size="11" runat="server"/>
		                                         </td>				                         	
			                                     <td class="label">Fecha 1º Vencimiento</td>
			                                     <td class="input">
			                                         <input id="txtFechavence" name="txtFechavence" type="text" class="css_input" value="" size="11" runat="server"/>
			                                     </td>
                                            </tr>					                 					         
                                        </table>
                                        
                                    </fieldset>   
                                    <fieldset>
                                        <legend  class="css_lbl_subTitulo">Tasas</legend>
       						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:100%">
       						                <colgroup>                           
                                                <col style="width:16.66%" />
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                            </colgroup>
   						                    <tr>
                                                <td class="label" >T.E.A. %</td>
				                                <td class="input" >				                                    
				                                    <input id="txtTEA" name="txtTEA" type="text" class="css_input" value="" size="15" runat="server"/>
				                                </td>
				                                <td class="label" >Costo de Fondos %</td>
				                                <td class="input" >
				                                    <input id="txtCostoFondos" name="txtCostoFondos" type="text" class="css_input" value="" size="15" runat="server"/>
				                                </td>
				                                <td class="label" >Spread %</td>
				                                <td class="input" >
				                                    <input id="txtSpread" name="txtSpread" type="text"  class="css_input" value="" size="15" runat="server" />
				                                    <img id="imgSpreadBanca" style="display:none;" src="../Util/images/ico_info.jpg" alt="No se encontró Spread de la Banca" title="No se encontró Spread de la Banca"/>		
				                                </td>
                                            </tr>						                        
                                            <tr>
				                                <td class="label" >Precuota %</td>
				                                <td class="input">
				                                    <input id="txtPrecuota"  name="txtPrecuota" type="text" class="css_input" value="" size="15" runat="server" />
				                                </td>
				                                <td class="label">Plazo de gracia de precuota</td>
					                            <td class="input">
					                                <input id="txtPlazoGraciaPrecuota" name="txtPlazoGraciaPrecuota" type="text" class="css_input" value="" size="15" runat="server" />
					                            </td>
						                    </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend  class="css_lbl_subTitulo">Comisiones</legend>
       						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:auto">
       						                <colgroup>                           
                                                <col style="width:16.66%" />
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                                                                                                                      
                                            </colgroup>
                                            <tr>
                                                <td></td>
						                        <th align="left" style="width:80px">Porcentaje</th>
						                        <th align="left" style="width:80px">Importes</th>
                                            </tr>
   						                    <tr>
   						                        <!-- Inicio IBK - Se agregan radio buttons para evitar que se modifiquen-->
                                                <td class="label">Opción de Compra (sin IGV)</td>
						                        <td class="input">
						                            <input type="hidden" name="hdOpCompra" id="hdOpCompra" value="1" runat="server" />
						                            <input id="rdbOpCompraProc" name="rdbOpCompraProc" type="radio" runat="server" onclick="javacript:fn_validaOpCompras('1');"/>
                                                    <input id="txtOpcionCompraPorc" name="txtOpcionCompraPorc" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>						                
						                        <td class="input">
						                            <input id="rdbOpCompraMonto" name="rdbOpCompraMonto" type="radio" runat="server" onclick="javacript:fn_validaOpCompras('0');"/>
						                            <input id="txtOpcionCompraMonto" name="txtOpcionCompraMonto" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>
                                            </tr>						                        
                                            <tr>
				                                <td class="label">Comision de Activación (sin IGV)</td>
						                        <td class="input">
						                            <input type="hidden" name="hdComiAct" id="hdComiAct" value="1" runat="server" />
						                            <input id="rdbComiActPorc" name="rdbComiActPorc" type="radio" runat="server" onclick="javacript:fn_validaComisionActivacion('1');"/>
						                            <input id="txtComisionActivacionProc" name="txtComisionActivacionProc" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>
						                        <td class="input">
						                            <input id="rdbComiActMonto" name="rdbComiActMonto" type="radio" runat="server" onclick="javacript:fn_validaComisionActivacion('0');"/>
						                            <input id="txtComisionActivacionMonto" name="txtComisionActivacionMonto" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>
						                    </tr>
						                    <tr>
						                        <td class="label">Comisión de Estructuración (sin IGV)</td>
						                        <td class="input">
						                        	<input type="hidden" name="hdComEstruc" id="hdComEstruc" value="1" runat="server" />
						                            <input id="rdbComEstrucPorc" name="rdbComEstrucPorc" type="radio" runat="server" onclick="javacript:fn_validaComisionEstructuracion('1');"/>
						                            <input id="txtComisionEstructuracionPorc" name="txtComisionEstructuracionPorc" type="text" class="css_input" value="" size="15" runat="server"/>
                                                </td>
						                        <td class="input">
						                            <input id="rdbComEstrucMonto" name="rdbComEstrucMonto" type="radio" runat="server" onclick="javacript:fn_validaComisionEstructuracion('0');"/>
						                            <input id="txtComisionEstructuracionMonto" name="txtComisionEstructuracionMonto" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>
						                    </tr>
						                    <!-- Fin IBK -->
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Seguro Bien</legend>
   						                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:100%">
   						                    <colgroup>                           
                                                <col style="width:16.66%" />
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                            </colgroup>
                                            <tr>    
                                                <td class="label">Tipo de Seguro</td>
                                                <td class="input">
                                                    <select id="cmbTipoBienSeguro" name="cmbTipoBienSeguro" runat="server">
					                                    <option value="0">-Seleccionar-</option>    					                               
					                                </select>
                                                </td>
                                                <td class="label" >Importe Prima</td>
						                        <td class="input" >
						                            <input id="txtImportePrimaSeguroBien" name="txtImportePrimaSeguroBien" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>
						                        <td class="label" >Nº Cuotas a Financiar</td>
						                        <td class="input" >
						                            <input id="txtNumCuotasfinanciadas" name="txtNumCuotasfinanciadas" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>
						                    </tr>       						         
                                        </table>
                                    </fieldset>
                                    <fieldset id="fld_SeguroDegravamen"><legend  class="css_lbl_subTitulo">Seguro Desgravamen</legend>
       						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%">
       						                <colgroup>                           
                                                <col style="width:16.66%" />
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                            </colgroup>
   						                    <tr>
   						                        <td class="label" >Tipo de Seguro</td>
                                                <td class="input" >
                                                    <select id="cmbTipoSeguro" name="cmbTipoSeguro" runat="server">
					                                    <option value="0">-Seleccionar-</option>    					                        
					                                </select>
                                                </td>
                                                <td class="label" >Importe Prima</td>
						                        <td class="input" >
						                            <input id="txtImportePrimaDesgravamen" name="txtImportePrimaDesgravamen" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>
						                        <td class="label" >Nº Cuotas a Financiar</td>
						                        <td class="input" >
						                            <input id="txtNumCuotaFinanciar" name="txtNumCuotaFinanciar" type="text" class="css_input" value="" size="15" runat="server"/>
						                        </td>
   						                    </tr>
       						            </table>
       						        </fieldset>        
 					            </div>
     					         
 					            <!-- **************** -->	
        			            <!-- TAB :: OPCIONES -->	 
        			            <!-- **************** -->	
					           <div id="tab-1">
						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">								     		            
						                <tr>						                
						                    <td class="label" style="width:180px;">Fecha de Ingreso</td>
						                    <td class="input" style="width:160px;">
						                        <input id="txtFechaIngreso" name="txtFechaIngreso" type="text" class="css_input" value="" size="10" runat="server"/>
						                    </td>
						                    <td class="label" style="width:250px;">¿Mostrar T.E.A en cartas?</td>
						                    <td class="input">
						                        <input type="hidden" name="hddMostrarTea" id="hddMostrarTea" value="0" runat="server" />
						                        <input id="rdbMostrarTeaSI" name="rdbMostrarTea" type="radio" runat="server" onclick="javacript:fn_validaMostrarTea('1');"/>SI  
                                                <input id="rdbMostrarTeaNO" name="rdbMostrarTea" type="radio" runat="server" onclick="javacript:fn_validaMostrarTea('0');"/>NO
                                            </td> 
						                </tr>
						                <tr>			                                 	
		                                    <td class="label" >Oferta Válida Hasta</td>
		                                    <td class="input" >
		                                            <input id="txtFechaOfertaValida" name="txtFechaOfertaValida" type="text" class="css_input" value="" size="15" runat="server"/>
		                                    </td>
		                                    <td class="label" >¿Mostrar Monto de Comisión?</td>
						                    <td class="input" >
						                         <input type="hidden" name="hddMostrarComision" id="hddMostrarComision" value="0" runat="server" />
						                         <input id="rdbMostrarComisionSI" name="rdbMostrarComision" type="radio" runat="server" onclick="javacript:fn_validaMostrarComision('1');"/>SI  
                                                 <input id="rdbMostrarComisionNO" name="rdbMostrarComision" type="radio" runat="server" onclick="javacript:fn_validaMostrarComision('0');"/>NO
                                            </td>
						                </tr>
						                <tr>    						               
						                    <td class="label">Periodo de Disponibilidad</td>
						                    <td class="input" colspan="3">
						                        <input id="txtPeriodoDisponibilidad" name="txtPeriodoDisponibilidad" type="text" class="css_input" value="" size="8" runat="server"/>
						                        Días
						                    </td>						                
						               </tr>
						               <tr> 
                                            <td class="label">Otras Comisiones</td>
						                    <td class="input" colspan="3">
                                                 <textarea id="txaOtrasComisiones" name="txaOtrasComisiones" cols="80" rows="3" runat="server" ></textarea>
						                    </td>
						               </tr>
						               <tr> 
                                            <td class="label">Proveedores</td>
						                    <td class="input" colspan="3">
                                                 <textarea id="txtProveedores" name="txtProveedores" cols="80" rows="3" runat="server" ></textarea>
						                    </td>
						               </tr>
						            </table>
  					           </div>
      					        
  					            <!-- ******************************* -->	
        			            <!-- TAB :: DOCUMENTOS -->	
        			            <!-- ******************************* -->	
					            <div id="tab-2" style="height:100%;">
						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="min-width:100%;">						                
    					                <tr>
    					                    <td colspan="4">  
    					                        <br />
    					                        
						                        <table cellpadding="0" cellspacing="0" border="0" style="width:880px;" id="tb_botonesDocumentos">
	                                                <tr>
    					                               <td align="left">  						                                
                                                                                                
                                                            <div id="dvBotonEliminaComentario" class="dv_img_boton_mini" style="border:0px">
                                                                <a href="javascript:fn_eliminarDocumentoComentario();">
                                                                    <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width:16px; height:16px;" border="0" align="bottom" />Eliminar
                                                                </a>
                                                            </div>  
                                                            <div id="dvBotonEditaComentario" class="dv_img_boton_mini" style="border:0px">
                                                                <a href="javascript:fn_editarDocumentoComentario();">
                                                                    <img alt="" src="../Util/images/ico_acc_editar.gif" style="width:16px; height:16px;" border="0" align="bottom"/>Editar
                                                                </a>
                                                            </div>  
                                                            <div id="dvBotonagregaComentario" class="dv_img_boton_mini" style="border:0px">
                                                                <a href="javascript:fn_abreNuevoDocumentoComentario();">
                                                                    <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width:16px; height:16px;" border="0" align="bottom"/>Agregar
                                                                </a>
                                                            </div> 
                                                             
                                                       </td>
                                                    </tr>  
	                                            </table>
        	                                    
	                                            <!--Inicia Carga Grilla -->	
                                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:850px">
                                                    <tr>
                                                    <td >
                                                        <table id="jqGrid_lista_A">
                                                        <tr><td></td></tr>
                                                        </table> 	
                                                    </td>
                                                    </tr>
                                                </table>
                                                <!--Finaliza Carga Grilla -->	
                                                
						                    </td>
				                        </tr>
				                    </table>
			                    </div>
    					        
					            <!-- ****************** -->	
					            <!-- TAB :: CRONOGRAMA  -->	
					            <!-- ****************** -->	
					            <div id="tab-3">
					                <!--Inicio IBK - Se permite bajar el cronograma a Word -->
					                <table id="Table1" border="0" cellpadding="0" cellspacing="3" style="width:100%;">
							          <tr>
                                         <td>
                                              <div id="dv_DescargarArchivoCronograma"></div>
                                         </td>
                                      </tr>
                                    </table>
					                <!-- Fin IBK -->                                                           
						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%;">
						                <tr>
						                    <td id="tbCronograma" style="text-align:center" >
                                                <table id="jqGrid_lista_C"><tr><td/></tr></table> 
                                                <div id="jqGrid_pager_C"></div>
						                    </td>
						                </tr>
						            </table>
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
