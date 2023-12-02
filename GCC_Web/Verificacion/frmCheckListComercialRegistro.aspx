<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCheckListComercialRegistro.aspx.vb" Inherits="Verificacion_frmCheckListComercialRegistro" %>

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
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>	

    <script src="../Util/js/jquery/jquery.watermarkinput.js" type="text/javascript"></script>
	
	<script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
    <!--<script src="../Util/js/jquery/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>-->
    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>
    
    
    <!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmCheckListComercialRegistro.aspx.js" > </script>
	
</head>
<body>
    <style>
        .defaultText {
            font-style: italic;
            color: #CCCCCC;
        }
   </style>
<form id="frmChecklistRegistro" runat="server">
    

    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo"   >
    	
    	<!-- ************************************************************************************ -->
        <!-- BOTONES -->
        <!-- ************************************************************************************ -->
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="icono">
				    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo" id="lbl_SubTitulo">Verificación</div>
				    <div class="css_lbl_titulo" id="lbl_titulo">CheckList Comercial ::  Editar</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">    				
				    <div id="dv_btnCancelar" class="dv_img_boton">
					    <a href="javascript:fn_cancelar();">
						    <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" title="Cancelar" /><br />
						    Volver
					    </a>
				    </div>
				    <div id="dv_btnGuardar" class="dv_img_boton">
					    <a href="javascript:fn_grabar();">
						    <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" title="Guardar" /><br />
						    Guardar
					    </a>
				    </div>
				    <div id="dv_btnGuardarEnviar" class="dv_img_boton" style="width:105px;">
					    <a href="javascript:fn_grabarYEnviar();">
						    <img alt="" src="../Util/images/ico_acc_grabarEnviar.gif" border="0" title="Guardar y Enviar" /><br />
						    Guardar y Enviar
					    </a>
				    </div>   
				    <div id="dv_btnSeguimiento" class="dv_img_boton" style="width:70px;">
				            <a href="javascript:fn_abreSeguimiento();">
					            <img alt="" src="../Util/images/ico_seguimiento.gif" border="0" /><br />
					            Seguimiento
				            </a>
			        </div>  
			            
		            <div id="dv_btnRetornaFlujo" class="dv_img_boton" style="width:70px;">
				         <a href="javascript:fn_retornarFlujo();">
					         <img alt="" src="../Util/images/ico_acc_retornar.gif" style="border: 0" /><br />
					         Retornar
			             </a>
		            </div>		
		            	            
			    </td>
		    </tr>
	    </table>	    	
    		
	    <div id="dv_contenedor" class="css_scrollPane" >			
	        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
		        <tr>
			        <td class="lineas"></td>			
		        </tr>
		        <tr>
		            <!-- INICIA CUERPO-->
                    <td class="cuerpo">
        				
				        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
					        <tr>
						        <td class="titulo css_lbl_tituloContenido">Datos de la Cotización</td>
						        <td class="botones">							
							        <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						        </td>
					        </tr>
				        </table>
				        				        
				        <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" value="" runat="server" />
				        <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" value="" runat="server" />
				        <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" value="" runat="server" />
				        <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" value="" runat="server" />
				        <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" value="" runat="server" />
				        <input type="hidden" id="hddFlagRetorno" name="hddFlagRetorno" value="" runat="server" />
				        <input type="hidden" id="hddPerfil" name="hddPerfil" value="" runat="server" />
				            
				        <div id="dv_datos"  class="dv_tabla_contenedora" >        					
					         <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%;" >
					            <colgroup>                           
                                        <col style="width:16.66%" />
                                        <col style="width:16.66%"/>                                                       
                                        <col style="width:16.66%"/>                                                       
                                        <col style="width:16.66%"/>                                                       
                                        <col style="width:16.66%"/>                                                       
                                        <col style="width:16.66%"/>                                                       
                                </colgroup>
						        <tr>
							        <td class="label">Nº Cotización</td>
							        <td class="input"><input id="txtNumeroCotizacion" name="txtNumeroCotizacion" type="text" class="css_input_inactivo" size="12" value="" disabled="disabled" readonly="readonly" runat="server" /></td>
							        <td class="label">Nº Contrato</td>
						            <td class="input"><input id="txtNumeroContrato"  name="txtNumeroContrato" type="text" class="css_input_inactivo" size="30" value="1" disabled="disabled" readonly="readonly" runat="server" /></td>
						            <td class="label">Procedencia</td>								    							    
							        <td class="input"><input id="txtProcedencia"  name="txtProcedencia" type="text" class="css_input_inactivo" size="30" value="" disabled="disabled" readonly="readonly" runat="server" /></td>
							    </tr>	
						        <tr>	
							        <td class="label">Clasificación del Bien</td>
							        <td class="input"><input id="txtclasificacionBien"  name="txtclasificacionBien" type="text" class="css_input_inactivo" size="30" value="" disabled="disabled" readonly="readonly" runat="server" /></td>							    
							        <td class="label" >Tipo de Bien</td>								    							    
							        <td class="input"><input id="txtTipoInmueble"  name="txtTipoInmueble" type="text" class="css_input_inactivo" size="30" value="" disabled="disabled" readonly="readonly"  runat="server"/></td>   
								    <td class="label" >Estado del Bien</td>								    							    
							        <td class="input"><input id="txtEstadoBien"  name="txtEstadoBien" type="text" class="css_input_inactivo" size="30" value="" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>  
							    </tr>
							    <tr>
							        <td class="label">CU Cliente</td>
							        <td class="input">
							        <input id="txtCuCliente" name="txtCuCliente" type="text" class="css_input_inactivo" size="12" value="" disabled="disabled" readonly="readonly"  runat="server"/>
							        <input id="hidCodUnico" type="hidden" runat="server" />
							        </td>
							        <td class="label">Razón Social o Nombre</td>
							        <!-- Inicio IBK - AAE - Libero la razón social para que sea modificable-->
							        <!--<td class="input"><input id="txtNombreRazonSocial_old" name="txtNombreRazonSocial_old" type="text" class="css_input_inactivo" size="30" value="" disabled="disabled" readonly="readonly" runat="server" /></td>-->
							        <td colspan="3" class="input"><input id="txtNombreRazonSocial" name="txtNombreRazonSocial" type="text" class="css_input" size="100" value="" runat="server" /></td>
								</tr>
								<tr>
								    <td class="label" >Tipo de Persona</td>								    							    
							        <td class="input"><input id="txtTipoPersona" name="txtTipoPersona" type="text" class="css_input_inactivo" size="30" value="" disabled="disabled" readonly="readonly" runat="server" /></td>    
								    <td class="label">Dirección</td>
								    <td colspan="3" class="input"><input id="txtDirCliente" name="txtDirCliente" type="text" class="css_input" size="100" value="" runat="server" /></td>
								</tr>
								<tr>
								    <td class="label">Nro Línea</td>                    								    
								    <td class="input">
								        <select id="cmbLinea" name="cmbLinea" class="" runat="server">
                                        </select>
                                    </td>
                                    <td class="label" >Fecha Activación</td>
                                    <td class="input">
                                        <input id="hidTipoCronograma" type="hidden" runat="server" />
                                        <input id="txtFechaMaxActivacion" name="txtFechaMaxActivacion" type="text" class="css_input" value="" size="11" runat="server"/>
                                    </td>	
                                    <td class="label" >Fecha Disponibilidad</td>
                                    <td class="input">
                                        <input id="txtFechaDisponibilidad" name="txtFechaDisponibilidad" type="text" class="css_input" value="" size="11" runat="server"/>
                                    </td>	
								</tr>
								<tr>
								    <td class="label">Opción de Compra (sin IGV)</td>
								    <td class="input">
								        <input id="hidOpcionCompra" type="hidden" runat="server" />
								        <input id="txtOpcionCompraMonto" name="txtOpcionCompraMonto" type="text" class="css_input" value="" size="15" runat="server"/></td>
	                                <td class="label">Com. Activación (sin IGV)</td>							 
	                                <td class="input">
	                                        <input id="hidComActivacion" type="hidden" runat="server" />
	                                        <input id="txtComisionActivacionMonto" name="txtComisionActivacionMonto" type="text" class="css_input" value="" size="15" runat="server"/></td>
	                                <td class="label">Com. Estructuración (sin IGV)</td>	                             
	                                <td class="input">
	                                    <input id="hidComestructuracion" type="hidden" runat="server" />
	                                    <input id="txtComisionEstructuracionMonto" name="txtComisionEstructuracionMonto" type="text" class="css_input" value="" size="15" runat="server"/></td>
								 </tr>
								 <!-- Fin IBK -->
								 <tr id="trMonedaContrato">
							        <td class="input"><input id="txtMonedaContrato" name="txtMonedaContrato" type="text" class="css_input_inactivo" size="12" value="" disabled="disabled" readonly="readonly"  runat="server"/></td>
							     </tr>
								 
                            </table>
                        </div>
                        
                        <!-- ************************************************************************* -->
                        <!-- TABS -->
                        <!-- ************************************************************************* -->    
    					<div id="divTabs" style="border:0px; background:none;">
                                <ul>
                                    <li><a href="#tab-0">UBICACIÓN DEL BIEN</a></li>
                                    <li><a href="#tab-1">CONDICIONES ADICIONALES</a></li>
                                    <li><a href="#tab-2">REPRESENTANTES A FIRMAR</a></li>
                                    <li><a href="#tab-3">CUENTAS DEL CLIENTE</a></li>
                                    <li><a href="#tab-4">DOCUMENTOS</a></li>
                                </ul>
                                
                                <!-- ****************************** -->
                                <!-- DATOS DEL BIEN -->	
                                <!-- ****************************** -->
                                <div id="tab-0" > 
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:80%;">                                           
                                        <tr>
                                            <td class="label">Ubicación</td>
                                            <td class="input" colspan="6">
                                                <input id="txtUbicacion" name="txtUbicacion" type="text" class="css_input" size="117" value="" runat="server" />
                                                <input id="hidUbicacion" runat="server" type="hidden" />
                                            </td>
                                            
                                        </tr>
                                        <!--
                                        RF1_1 - IJM - 04/09/2012
                                        Motivo :: se aumento Combos Departamento-Provincia-Distrito
                                        -->
                                        <tr>
                                            <td id="td3"></td> 
                                            <td class="label" id="td1">Departamento</td>
                                            <td class="input" id="td2">
                                                <select id="cmbDepartamentoUbicacion" name="cmbDepartamentoUbicacion" onchange="javascript:fn_cargaComboProvinciaUbicacion(this.value);" runat="server">
                                                    <option value="0">[-Seleccionar-]</option>                                                    
                                                </select>
                                            </td>
                                            <td class="label">Provincia</td>
                                            <td class="input">
                                                <select id="cmbProvinciaUbicacion" name="cmbProvinciaUbicacion" onchange="javascript:fn_cargaComboDistritoUbicacion(cmbDepartamentoUbicacion.value,this.value);" runat="server"  >
                                                    <option value="0">[-Seleccionar-]</option>                                                    
                                                </select>
                                            </td>
                                            <td class="label">Distrito</td>
                                            <td class="input">
                                                <select id="cmbDistritoUbicacion" name="cmbDistritoUbicacion" runat="server">
                                                   <option value="0">[-Seleccionar-]</option>                                                  
                                                </select>
                                            </td>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
                                        </tr>     
                                        <tr>    
                                            <td class="label">Uso</td>
                                            <td class="input" colspan="6"><input id="txtUso" name="txtUso" type="text" class="css_input" size="117" value="" runat="server" /></td>
                                        </tr>  
                                    </table> 
                                </div>
				                
				                <!-- ****************************** -->
                                <!-- CONDICIONES ADICIONALES -->	
                                <!-- ****************************** -->
                                <div id="tab-1">
                                    <br />
                                    
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0"  style="width:auto;">                                       
                                        <tr>
                                            <td>                                                                                   
                                                <div id="dv_btnAgregarConAdi" class="dv_img_boton_mini" style="border:0">
                                                    <a href="javascript:fn_AgregarCondicionesAdicionales();" style="display:inline;">
                                                        <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width:16px; height:16px;display:inline;" border="0"/>Agregar
                                                    </a>
                                                </div>    
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td>
                                               <table id="jqGrid_lista_E"><tr><td/></tr></table>   
                                                <div id="jqGrid_pager_E" align="center"> </div>      
                                            </td>
                                        </tr>
                                    </table>	                                                                       
                                </div>
                                 
                                
                                <!-- ****************************** -->
                                <!-- REPRESENTANTES A FIRMAR -->	
                                <!-- ****************************** -->    
				                <div id="tab-2" >	
				                    <fieldset>
                                    <legend  class="css_lbl_subTitulo">Representantes Interbank</legend>    	
                                                     
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" >
                                         <tr>
                                            <td style="width:150px;"></td>
                                            <td style="width:250px;"></td>
                                            <td style="width:150px;"></td>
                                            <td style="width:250px;"></td>
                                        </tr>     
                                                                         
                                        <tr>
                                            <td class="label">Contrato Firmarse en </td>			                                        
                                            <td class="input">
                                                <select id="cmbcontratofirma" name="cmbcontratofirma" runat="server" onchange="javascript:fn_ubicacionfirmar(this.value);" >
                                                </select>
                                            </td>
                                            
                                            <td class="label" id="td_lblDepartamento">Departamento</td>
                                            <td class="input" id="td_InpDepartamento">
                                                <select id="cmbDepartamento" name="cmbDepartamento" onchange="javascript:fn_cargaComboProvincia(this.value);" runat="server">
                                                    <option value="0">[-Seleccionar-]</option>                                                    
                                                </select>
                                            </td>
                                        </tr>
                                        <tr id="tr_ProvDist" >
                                            <td class="label">Provincia</td>
                                            <td class="input">
                                                  
                                                <select id="cmbProvincia" name="cmbProvincia" onchange="javascript:fn_cargaComboDistrito(cmbDepartamento.value,this.value);"  >
                                                    <option value="0">[-Seleccionar-]</option>                                                    
                                                </select>
                                            </td>
                                            <td class="label">Distrito</td>
                                            <td class="input">
                                                <select id="cmbDistrito" name="cmbDistrito">
                                                   <option value="0">[-Seleccionar-]</option>                                                  
                                                </select>
                                            </td>
                                        </tr>
                                    </table>
                                   
   
                                    <div id="dv_representanteslima" style="border:0px;"> 
                                      
                                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="5" align="left" style="width:auto;">
                                                    <tr id ="tragregarrepresentante" style="vertical-align:top" >
                                                    <td>
                                                        <a href="javascript:fn_AgregarRepresentantes();">
                                                            <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width:16px; height:16px;display:inline;" border="0"/>
                                                            Agregar Representantes
                                                        </a>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    </tr>
                                                    				
                                                <tr>
                                                <td  style="vertical-align:top" >
                                                    <table id="jqGrid_lista_C"><tr><td/></tr></table> 
                                                    <div id="jqGrid_pager_C"></div>   
                                                </td>
                                                <td style="width:25px;">						                                    
                                                    <a href="#"><img alt="" src="../Util/images/ico_acc_lstAdd.gif" border="0" onclick="javascript:fn_AgregarRepresentante();" /></a>			                                                
                                                    <a href="#"><img alt="" src="../Util/images/ico_acc_lstRemove.gif" border="0" onclick="javascript:fn_EliminarRepresentante();"/></a>	                                                
                                                </td>
                                                <td style="vertical-align:top" >
                                                     <table id="jqGrid_lista_B"><tr><td/></tr></table> 
                                                     <div id="jqGrid_pager_B"> </div>   
                                                </td>  
                                                 </tr>
                                            </table> 
                                      
                                    </div>    
                                    </fieldset>                                             
                                    <div id="dv_representantesprovincia" style="border:0px;">
                                        <fieldset > 
                                            <legend   class="css_lbl_subTitulo">Representantes Provincia</legend>  

                                            <table  id="tb_formulario" border="0" cellpadding="0" cellspacing="5" align="left" style="width:auto;">	                                            
                                                <tr style="vertical-align:top">
                                                    <td colspan="2">
                                                        <a href="javascript:fn_Representantes(0);">
                                                            <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width:16px; height:16px;display:inline;" border="0"/>
                                                            Agregar Representantes
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align:top">
                                                    <td>
                                                        <table id="jqGrid_lista_A"><tr><td/></tr></table> 
                                                        <div id="jqGrid_pager_A"> </div>  
                                                    </td>
                                                    <td style="width:25px; text-align:center;">						                                    
                                                        <a href="#"><img alt="" src="../Util/images/ico_acc_lstAdd.gif" border="0" onclick="javascript:fn_AgregarRepresentante();" /></a>			                                                
                                                        <a href="#"><img alt="" src="../Util/images/ico_acc_lstRemove.gif" border="0" onclick="javascript:fn_EliminarRepresentante();"/></a>	                                                
                                                    </td>
                                                    <td>
                                                        <table id="jqGrid_lista_D"><tr><td/></tr></table> 
                                                        <div id="jqGrid_pager_D"> </div>  
                                                    </td>  
                                                </tr>
                                            </table> 
                                            
                                        </fieldset > 
                                    </div>
                                
                                </div>
 				                  
 				                <!-- ****************************** -->
                                <!-- CUENTAS DEL CLIENTE -->	
                                <!-- ****************************** -->                                     
                                <div id="tab-3">                                    
                                    <table  id="tb_formulario" border="0" cellpadding="0" cellspacing="5"  style="width:100%;" >				
                                        <tr>
                                            <td class="label">Tipo de Cuenta 1</td>
                                            <td class="input">
                                                <select id="cmbTipoCuenta1" name="cmbTipoCuenta1" runat="server">
                                                    <option>-Seleccionar-</option>
                                                   
                                                </select>
                                            </td>
                                            <td class="label">Moneda</td>
                                            <td class="input">
                                                <select id="cmbMoneda1" name="cmbMoneda1" runat="server">
                                                    <option>-Seleccionar-</option>
                                                    </select>
                                            </td>
                                            <td class="label">Nº Cuenta</td>
                                            <td class="input"><input id="txtNumeroCuenta1" name="txtNumeroCuenta1" type="text" class="css_input" size="20" value="" runat="server" /></td>
                                        </tr>    
                                        <tr>
                                            <td class="label">Tipo de Cuenta 2</td>
                                            <td class="input">
                                                <select id="cmbTipoCuenta2" name="cmbTipoCuenta2" runat="server">
                                                    <option>-Seleccionar-</option>
                                                </select>
                                            </td>
                                            <td class="label">Moneda</td>
                                            <td class="input">
                                                <select id="cmbMoneda2" name="cmbMoneda2" runat="server">
                                                    <option>-Seleccionar-</option>
                                                    
                                                </select>
                                            </td>
                                            <td class="label">Nº Cuenta</td>
                                            <td class="input"><input id="txtNumeroCuenta2" name="txtNumeroCuenta2" type="text" class="css_input" size="20" value="" runat="server" /></td>
                                        </tr>
                                    </table>                           
                                </div>
				                <!-- ****************************** -->
                                <!-- DOCUMENTOS -->	
                                <!-- ****************************** -->                                    
                                <div id="tab-4">                                
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="2" style="width:80%;">
                                    <!--
                                        <tr>
                                            <td class="label">Nombre Documento</td>
                                            <td class="input"><input id="txtDocumento" name="txtDocumento" type="text" class="css_input" size="35" value="" /></td>
                                            <td class="label">Archivo</td>
                                            <td class="input"><input id="txtRutaArchivoDocumento" name="txtRutaArchivoDocumento" type="file" class="css_input" size="50" value="" /></td>
                                        </tr>  
                                      -->    
                                    </table>
                                    <table>          
                                        <tr>
                                            <td align="left">  					                                            
                                                <div id="Div16" class="dv_img_boton_mini" style="border:0">
                                                    <a href="javascript:fn_AgregarDocumentos();" style="display:inline;">
                                                        <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width:16px; height:16px;display:inline;" border="0"/>Agregar
                                                    </a>
                                                </div>                                     
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td>
                                                <table id="jqGrid_lista_I"><tr><td/></tr></table> 
                                                <div id="jqGrid_pager_I"> </div>  
                                            </td>
                                        </tr>
                                    </table>	                                    
                                </div>
                                <!--Fin !-->	  
				         </div>
        		    </td>
        		    <!-- FIN CUERPO-->   
    			</tr>
    	    </table>	
        </div>	
     </div> 
     <!-- FIN CUERPO -->     
    
        <!--Agrega a la lista de Representantes-->
        <input id="hddCodigoRepresentante" type="hidden" />
        <input id="hddNumeroContrato"   type="hidden" />
        <input id="hddCodigoTipoRepresentante" type="hidden" />
        
        <!--Elimina Representantes Agregados-->
        <input id="hddCodigoRepresentanteElimina" type="hidden" />
        <input id="hddNumeroContratoElimina"   type="hidden" />
        <input id="hddCodigoTipoRepresentanteElimina" type="hidden" />
        <!--Elimina DocumentosContrato-->
        <input id="hddCodigo" name="hddCodigo" type="hidden" />
        <input id="hddFirmaen" name="hddFirmaen" type="hidden" runat="server" />
        <input id="hddconsOrigenCondicion" name="hddconsOrigenCondicion" type="hidden" runat="server" />
        <input id="hddTotalRegistrosProveedor" name="hddTotalRegistrosProveedor" type="hidden" runat="server" />
       
        <input id="hddFlagVerificaAdjunto" name="hddFlagVerificaAdjunto" type="hidden" runat="server" />
        <input id="hddTotalRegistros" name="hddTotalRegistros" type="hidden" runat="server" />
        <input id="hddTotalValidadados" name="hddTotalValidadados" type="hidden" runat="server" />
        
        <input type="button" name="btnBuscar" id="btnBuscar" onclick="javascript:fn_cargaDocumentos();" style="display:none;"/>
        <input id="hddValidaCuenta" name="hddValidaCuenta" type="hidden" runat="server" />
        <input id="hddValidaCuenta2" name="hddValidaCuenta2" type="hidden" runat="server" />
        <input id="hddUbigeoUbicacion" name="hddUbigeoUbicacion" type="hidden" runat="server" />
        
        <input type="button" name="cmdListarDocumentos" id="cmdListarDocumentos" onclick="javascript:fn_cargaDocumentos();" style="display:none;" />
  
</form>

</body>
</html>