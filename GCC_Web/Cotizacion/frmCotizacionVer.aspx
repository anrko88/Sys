<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCotizacionVer.aspx.vb" Inherits="Cotizacion_frmCotizacionVer" %>

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
	<script type='text/javascript' src="frmCotizacionVer.aspx.js"></script>
	
</head>
<body>
    <form id="frmCotizacionVer" runat="server">
    
        <input type="hidden" id="hddCodigoCotizacion" name="hddCodigoCotizacion" value="" runat="server" />
        <input type="hidden" id="hddVersionCotizacion" name="hddVersionCotizacion" value="" runat="server" />
        <input type="hidden" name="hddMostrarTea" id="hddMostrarTea" value="0" runat="server" />
        <input type="hidden" name="hddMostrarComision" id="hddMostrarComision" value="0" runat="server" />
    
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
            
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
	    <tr>
		    <td class="icono">
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/Util/images/ico_mdl_cotizacion.gif" />
            </td>
		    <td class="titulos">
			    <div class="css_lbl_subTitulo">Cotización y Negociación</div>
			    <div class="css_lbl_titulo">Ver</div>
		    </td>
		    <td class="espacio">&nbsp;</td>
		    <td class="botones">
				    <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:parent.fn_util_CierraModal();">
					        <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
					        Volver
				        </a>
			        </div>
		    </td>
	    </tr>
	    </table>
	    <div class="css_scrollPane" style="height:480px">
	    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >
	    <tr>
		    <td>		    
		    <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
				            
				            
				        <input type="hidden" id="hddTipoPersona" name="hddTipoPersona" value="" runat="server" />
				            
    			        <!-- ******************************************************************************************** -->	
    			        <!-- Inicia de Cabecera -->
    			        <!-- ******************************************************************************************** -->	        			    
			            <div id="dv_datos" class="dv_tabla_contenedora">
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%">                                                   					        
                        <tr>
                            <td class="label">Nro. Cotización</td>
                            <td>
                                <asp:Label ID="txtNumeroCotizacion" runat="server"></asp:Label>
                            </td>
                            <td class="label">Estado</td>
                            <td>
                                <asp:Label ID="lblEstado" runat="server"></asp:Label>
                            </td>                            
                            <td class="label">Generar Carta</td>
                            <td>
                               <asp:CheckBox ID="chkGeneraCarta" runat="server" Enabled="false"/>
                               <label id="lblFechacarta" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">CU Cliente</td>
                            <td>
                                <asp:Label ID="txtCUCliente" runat="server"></asp:Label>
                            </td>
                            <td class="label">Razón Social o Nombre</td>
                            <td>
                                <asp:Label ID="txtNombreCliente" runat="server"></asp:Label>                                
                            </td>
                            <td class="label">Tipo Persona</td>
                            <td>
                                <asp:Label ID="lblTipoPersona" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Tipo de Documento</td>
                            <td>
                                <asp:Label ID="lblTipoDocumento" runat="server"></asp:Label>
                            </td>                            
                            <td class="label">Nro. Documento</td>
                            <td>
                                <asp:Label ID="lblNumeroDocumento" runat="server"></asp:Label>                                
                            </td>                            
                            <td class="label">
                                <asp:CheckBox ID="chkLinea" runat="server" Enabled="False" />Nº Linea
                            </td>
                            <td>
                               <asp:Label ID="lblLinea" runat="server"></asp:Label>
                            </td>                            
                        </tr>
                        <tr>
                            <td class="label">Ejecutivo de la Banca</td>
                            <td>
                                <asp:Label ID="lblEjecutivoBanca" runat="server"></asp:Label>
                            </td>
                            <td class="label">Banca en atención</td>
                            <td>
                                <asp:Label ID="lblBanca" runat="server"></asp:Label>                                
                            </td>                           
                            <td class="label">Zonal</td>
                            <td>
                                <asp:Label ID="lblZonal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Contacto</td>
                            <td>
                                <asp:Label ID="lblContacto" runat="server"></asp:Label>
                            </td>
                            <td class="label">Correo</td>
                            <td>
                                <asp:Label ID="lblCorreo" runat="server"></asp:Label>
                            </td>					                
                            <td class="label">Ejecutivo Leasing</td>
                            <td>
                                <asp:Label ID="lblEjecutivoLeasing" runat="server"></asp:Label>                                
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
				            <li><a href="#tab-2">DOCUMENTOS Y COMENTARIOS</a></li>
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
                                    <col class="label" style="width:16.66%" />
                                    <col class="input" style="width:16.66%"/>                                                       
                                    <col class="label" style="width:16.66%"/>                                                       
                                    <col class="input" style="width:16.66%"/>                                                       
                                    <col class="label" style="width:16.66%"/>                                                       
                                    <col class="input" style="width:16.66%"/>                                                       
                                </colgroup>
				                <tr>  
                                    <td class="label">Tipo de Contrato</td>
				                    <td>
				                        <asp:Label ID="lblTipoContrato" runat="server" Width ="150px"></asp:Label>
				                        <asp:Label ID="lblSubTipoContrato" runat="server" Width ="150px"></asp:Label>
			                        </td>
			                        <td class="label">Moneda</td>
				                    <td>
				                        <asp:Label ID="lblMoneda" runat="server" Width ="150px"></asp:Label>
				                     </td>      						                 
                                     <td class="label">Procedencia</td>
                                     <td>
                                        <asp:Label ID="lblProcedencia" runat="server" Width ="150px"></asp:Label>
                                    </td>                                            
                                </tr>
                                <tr>
                                    <td class="label">Clasificación del Bien</td>
                                    <td>
                                        <asp:Label ID="lblClasificacionBien" runat="server" Width ="150px"></asp:Label>
                                    </td>
                                    <td class="label">Tipo de Bien</td>
				                    <td>
				                        <asp:Label ID="lblTipoBien" runat="server" Width ="150px"></asp:Label>
				                    </td>                                
				                    <td class="label">Estado del bien</td>
			                        <td>
			                            <asp:Label ID="lblEstadoBien" runat="server" Width ="150px"></asp:Label>
			                        </td>
                                </tr>
                                <tr>
                                    <td class="label">Precio Venta</td>  
			                        <td>
			                            <asp:Label ID="lblPrecioVenta" runat="server" Width ="100px"></asp:Label>
			                        </td>   						                     
			                        <td class="label">IGV</td>
				                    <td>
				                        <asp:Label ID="lblMontoIGV" runat="server" Width ="100px"></asp:Label>
				                    </td>	
				                    <td class="label">Valor Venta</td>
				                    <td>
				                        <asp:Label ID="lblValorVenta" runat="server" Width ="100px"></asp:Label>
				                    </td>					                     
			                     </tr>
			                     <tr>
			                        <td class="label">Cuota Inicial</td>
			                        <td>
			                            <asp:Label ID="lblCuotaInicial" runat="server" Width ="100px"></asp:Label>
			                        </td> 
			                        <td class="label">Cuota Inicial %</td>
				                    <td>
				                        <asp:Label ID="lblCuotaIncialPorc" runat="server" Width ="100px"></asp:Label>
				                    </td>
				                    <td class="label">Riesgo Neto</td>
			                        <td>
			                            <asp:Label ID="lblRiesgoNeto" runat="server" Width ="100px"></asp:Label>
			                        </td>
				                 </tr>
				                </table>
				            </fieldset>
				            
				            <fieldset>
				                <legend  class="css_lbl_subTitulo">Datos Cronograma</legend>					            
					            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:100%">
				                <colgroup>                           
                                    <col class="label" style="width:16.66%" />
                                    <col class="input" style="width:16.66%"/>                                                       
                                    <col class="label" style="width:16.66%"/>                                                       
                                    <col class="input" style="width:16.66%"/>                                                       
                                    <col class="label" style="width:16.66%"/>                                                       
                                    <col class="input" style="width:16.66%"/>                                                       
                                </colgroup>
		                        <tr>
	                                <td class="label">Tipo de Cronograma</td>
                                    <td>
                                        <asp:Label ID="lblTipoCronograma" runat="server" Width ="150px"></asp:Label>
                                    </td>
                                    <td class="label">Nro. Cuotas</td>
                                    <td>
                                        <asp:Label ID="lblNroCuotas" runat="server" Width ="50px"></asp:Label>		                                        
                                    </td>
                                    <td class="label">Periodicidad</td>
                                    <td>
                                        <asp:Label ID="lblPeriocidad" runat="server" Width ="150px"></asp:Label>
                                    </td>
	                            </tr>
	                            <tr> 
	                                 <td class="label">Frecuencia de Pago</td>
				                     <td>
				                        <asp:Label ID="lblFrecuenciaPago" runat="server" Width ="150px"></asp:Label>
				                     </td>
				                     <td class="label">Plazo Gracia</td>
                                     <td>
                                        <asp:Label ID="lblPlazoGracia" runat="server" Width ="150px"></asp:Label>
                                     </td>
                                     <td class="label">Tipo de Gracia</td>
                                     <td>
                                        <asp:Label ID="lblTipoGracia" runat="server" Width ="150px"></asp:Label>
                                     </td>                               
		                        </tr>
		                        <tr>	
		                            <td class="label" >Fecha Activación</td>
                                     <td class="input">                                         
                                        <asp:Label ID="lblFechaMaxActivacion" runat="server" Width ="150px"></asp:Label>
                                     </td>					                         	
                                     <td class="label">Fecha 1º Vencimiento</td>
                                     <td>
                                        <asp:Label ID="lblFechaVence" runat="server" Width ="150px"></asp:Label>
                                     </td>
                                </tr>					                 					         
                                </table>                                    
                            </fieldset> 
                              
                            <fieldset>
                                <legend  class="css_lbl_subTitulo">Tasas</legend>
					            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:100%">				                
			                    <tr>
                                    <td class="label">T.E.A. %</td>
	                                <td>
	                                    <asp:Label ID="lblTEA" runat="server" Width ="80px"></asp:Label>
	                                </td>
	                                <td class="label">Costo de Fondos %</td>
	                                <td>
	                                    <asp:Label ID="lblCostoFondos" runat="server" Width ="80px"></asp:Label>
	                                </td>
	                                <td class="label">Spread %</td>
	                                <td>
	                                    <asp:Label ID="lblSpread" runat="server" Width ="80px"></asp:Label>
	                                </td>
                                </tr>
                                <tr>
	                                <td class="label">Precuota %</td>
	                                <td>
	                                    <asp:Label ID="lblPreCuota" runat="server" Width ="80px"></asp:Label>
	                                </td>
	                                <td class="label">Plazo de gracia de precuota</td>
		                            <td>
		                                <asp:Label ID="lblPlazpGraciaPreCuota" runat="server" Width ="80px"></asp:Label>				                                
		                            </td>
			                    </tr>
                                </table>
                            </fieldset>
                            
                            <fieldset>
                                <legend  class="css_lbl_subTitulo">Comisiones</legend>
					            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:410px">				       
                                <tr>
                                    <td style="width:250px"></td>
			                        <th class="left" style="width:80px; text-align:right;">Porcentaje</th>
			                        <th class="left" style="width:80px; text-align:right;">Importes</th>
                                </tr>
			                    <tr>
                                    <td class="label">Opción de Compra (sin IGV)</td>
			                        <td class="input" style="text-align:right;">
			                            <asp:Label ID="lblOpcionCompraPorc" runat="server" Width ="80px"></asp:Label>
			                        </td>						                
			                        <td class="input" style="text-align:right;">
			                            <asp:Label ID="lblOpcionCompraMonto" runat="server" Width ="80px"></asp:Label>
			                        </td>
                                </tr>						                        
                                <tr>
	                                <td class="label">Comision de Activación (sin IGV)</td>
			                        <td class="input" style="text-align:right;">
			                            <asp:Label ID="lblComisionActivacionProc" runat="server" Width ="80px"></asp:Label>
			                        </td>
			                        <td class="input" style="text-align:right;">
			                            <asp:Label ID="lblComisionActivacionMonto" runat="server" Width ="80px"></asp:Label>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td class="label">Comisión de Estructuración (sin IGV)</td>
			                        <td class="input" style="text-align:right;">
			                            <asp:Label ID="lblComisionEstructuracionPorc" runat="server" Width ="80px"></asp:Label>
                                    </td>
			                        <td class="input" style="text-align:right;">
			                            <asp:Label ID="lblComisionEstructuracionMonto" runat="server" Width ="80px"></asp:Label>
			                        </td>
			                    </tr>
                                </table>
                            </fieldset>
                            
                            <fieldset>
                                <legend class="css_lbl_subTitulo">Seguro Bien</legend>
				                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:100%">			                
                                <tr>    
                                    <td class="label">Tipo de Seguro</td>
                                    <td class="input">
                                        <asp:Label ID="lblTipoBienSeguro" runat="server" Width ="150px"></asp:Label>
                                    </td>
                                    <td class="label" >Importe Prima</td>
			                        <td class="input" >
			                            <asp:Label ID="lblImportePrimaSeguroBien" runat="server" Width ="150px"></asp:Label>
			                        </td>
			                        <td class="label" >Nº Cuotas a Financiar</td>
			                        <td class="input" >
			                            <asp:Label ID="lblNumCuotasfinanciadas" runat="server" Width ="150px"></asp:Label>
			                        </td>
			                    </tr>
                                </table>
                            </fieldset>
                            
                            <fieldset id="fld_SeguroDegravamen">
                                <legend  class="css_lbl_subTitulo">Seguro Desgravamen</legend>
					            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%">				           
			                    <tr>
			                        <td class="label">Tipo de Seguro</td>
                                    <td>
                                        <asp:Label ID="lblTipoSeguro" runat="server" Width ="150px"></asp:Label>
                                    </td>
                                    <td class="label">Importe Prima</td>
			                        <td>
			                            <asp:Label ID="lblImportePrimaDesgravamen" runat="server" Width ="150px"></asp:Label>
			                        </td>
			                        <td class="label">Nº Cuotas a Financiar</td>
			                        <td>
			                            <asp:Label ID="lblNumCuotaFinanciar" runat="server" Width ="150px"></asp:Label>
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
                                    <asp:Label ID="lblFechaIngreso" runat="server" Width="100px"></asp:Label>   
			                    </td>
			                    <td class="label" style="width:250px;">¿Mostrar T.E.A en cartas?</td>
			                    <td class="input">
                                    <input id="rdbMostrarTeaSI" name="rdbMostrarTea" type="radio" runat="server" onclick="javacript:fn_validaMostrarTea('1');" disabled="disabled"/>SI  
                                    <input id="rdbMostrarTeaNO" name="rdbMostrarTea" type="radio" runat="server" onclick="javacript:fn_validaMostrarTea('0');" disabled="disabled"/>NO	                       
                                </td> 
			                </tr>
			                <tr>			                                 	
                                <td class="label" >Oferta Válida Hasta</td>
                                <td class="input" >
                                    <asp:Label ID="lblfechaOfertaValida" runat="server" Width="100px"></asp:Label>  
                                </td>
                                <td class="label" style="width:250px;">¿Mostrar Monto de Comisión?</td>
			                    <td class="input" >
			                        <input id="rdbMostrarComisionSI" name="rdbMostrarComision" type="radio" runat="server" onclick="javacript:fn_validaMostrarComision('1');" disabled="disabled"/>SI  
                                    <input id="rdbMostrarComisionNO" name="rdbMostrarComision" type="radio" runat="server" onclick="javacript:fn_validaMostrarComision('0');" disabled="disabled"/>NO                        
                                </td>
			                </tr>
			                <tr>    						               
			                    <td class="label">Periodo de Disponibilidad</td>
			                    <td class="input" colspan="3">
                                    <asp:Label ID="lblPeriodoDisponibilidad" runat="server"></asp:Label>
			                    </td>
			               </tr>			               
			               <tr> 
                                <td class="label">Otras Comisiones</td>
			                    <td class="input" colspan="3">
			                         <textarea id="txaOtrasComisiones" name="txaOtrasComisiones" cols="80" rows="3" runat="server" class="css_input_inactivo" ></textarea>
			                    </td>
			               </tr>
			               <tr> 
                                <td class="label">Proveedores</td>
			                    <td class="input" colspan="3">
                                     <textarea id="txtProveedores" name="txtProveedores" cols="80" rows="3" runat="server" class="css_input_inactivo"></textarea>
			                    </td>
			               </tr>
				           </table>
			            </div>
  					        
			            <!-- ******************************* -->	
			            <!-- TAB :: DOCUMENTOS -->	
			            <!-- ******************************* -->	
			            <div id="tab-2">
				            <table border="0" cellpadding="0" cellspacing="3" style="width:100%;">
			                <tr>
			                    <td  style="text-align:left" >
			                        <table id="jqGrid_lista_A"><tr><td/></tr></table> 
		                            <div id="jqGrid_pager_A"></div>
			                    </td>
	                        </tr>
		                    </table>
	                    </div>
					        
			            <!-- ****************** -->	
			            <!-- TAB :: CRONOGRAMA  -->	
			            <!-- ****************** -->	
			            <div id="tab-3" style="padding-left:2px; padding-right:2px;">                                
				            <table border="0" cellpadding="0" cellspacing="1" style="width:100%;">
			                <tr>
			                    <td style="text-align:center" >
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
            </td>
        </tr>
	    </table>
	    </div> 
      </div>
      
    </form>
</body>
</html>
