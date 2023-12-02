<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDesembolsoBienesdetalle.aspx.vb" Inherits="Desembolso_frmDesembolsoBienesdetalle"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">

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
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>
			
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmDesembolsoBienesdetalle.aspx.js"></script>
	
</head>
<body>
    
<form runat="server" id="frmDesembolsoBienesDetalle">

    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
	            <td class="icono">
	                <img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono"/>		
	            </td>
	            <td class="titulos">
		            <div class="css_lbl_subTitulo">Desembolso</div>
		            <div class="css_lbl_titulo">Desembolso: Nuevo</div>
	            </td>
	            <td class="espacio">&nbsp;</td>
	            <td class="botones">
			    <div id="dv_img_boton" class="dv_img_boton">
			       <a href="javascript:parent.fn_util_CierraModal();">
			            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
				        Cancelar
			        </a>
		        </div>
			    <div id="dv_img_boton" class="dv_img_boton">
			        <a href="javascript:fn_grabar();">
				        <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
				        Guardar
			        </a>
		        </div>
	        </td>
            </tr>
         </table>	
            
            
        <div id="dv_contenedor" class="css_scrollPane">
	    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" >
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
		    <tr>
			    <td class="cuerpo">
    				
				    <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
					    <tr>
						    <td class="titulo css_lbl_tituloContenido">Inmueble</td>
						    <td class="botones">							
							    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						    </td>
					    </tr>
				    </table>
				    
    				 <!-- *********************************************************************
				    Mantenimiento de Bienes
				          *********************************************************************
				    -->
				    <div id="dv_datos_bien" class="dv_tabla_contenedora">
					    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:800px;">
						    <tr>
                                <td class="label">N° Contrato</td>
                                <td class="input"><input id="txtNroCotizacion" type="text" class="css_input_inactivo" size="12" value="" /></td>
                                <td class="label">Estado</td>
                                <td class="input"><input id="txtEstado" type="text" class="css_input_inactivo" size="12" value="" /></td>
                            </tr>
                            <tr>
                                <td class="label">CU Cliente</td>
                                <td class="input"><input id="txtContrato5" type="text" class="css_input_inactivo" size="15"/></td>
                                <td class="label">Razón Social</td>
                                <td class="input"><input id="txtContrato6" type="text" class="css_input_inactivo" size="75"/></td>
                            </tr> 
					    </table>
				    </div>
			        <br />
			        <table border="0" cellpadding="0" cellspacing="0">
					    <tr>
						    <td class="titulo css_lbl_tituloContenido">Registro</td>
					    </tr>
				    </table>
				    
				    <div id="dv_datos_bienes" class="dv_tabla_contenedora">
					    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
						    <tr>
							    <td class="label">Descripción</td>
							    <td class="input"><input id="txtDescripcionDemanda" type="text" class="css_input" size="50" value=""/></td>	
							    <td class="label">Partida Registral</td>
							    <td class="input"><input id="Text3" type="text" class="css_input" size="11" value=""/></td>
							</tr>    
							<tr>
							    <td class="label">Distrito</td>
							    <td class="input">
							        <select id="Select2" >
                                        <option>- Seleccione -</option>
                                        <option>Ate</option>
                                        <option>San Borja</option>
                                        <option>La Molina</option>
                                        <option>San Isidro</option>
                                    </select>
							    </td>	
							    <td class="label">Provincia</td>
							    <td class="input">
							        <select id="Select3">
                                        <option>- Seleccione -</option>
                                        <option>Lima</option>
                                        <option>Callao</option>
                                    </select>
							    </td>
							    <td class="label">Departamento</td>
							    <td class="input">
							        <select id="Select4">
                                        <option>- Seleccione -</option>
                                        <option>Lima</option>                                        
                                        <option>Arequipa</option>                                        
                                        <option>Tacna</option>                                        
                                        <option>Ica</option>                                        
                                    </select>
							    </td>							    
						    </tr>
    					    <tr>
							    <td class="label">Tipo Inmueble</td>
							    <td class="input" colspan="5">
                                    <select id="Select1" style="width:200px;">
                                        <option>- Seleccione -</option>
                                        <option>Bien Futuro</option>
                                        <option>Compra Venta</option>
                                        <option>Edificacion</option>
                                    </select>
							    </td>							    
						    </tr>
						    <tr>
						         <td class="label">Fecha de Transferencia</td>
						         <td class="input" colspan="5"><input id="txtFechatransferencia" type="text" class="css_input" size="11" value=""/></td>
						    </tr>
						    <tr>
						        <td class="label">Ruc Proveedor</td>
						        <td class="input"><input id="Text6" type="text" class="css_input" size="11" value=""/></td>
						        <td class="label">Nombre</td>
						        <td class="input" colspan="3"><input id="Text7" type="text" class="css_input" size="50" value=""/></td>
						    </tr>
    					    <tr>
						        <td class="label">Observaciones</td>
						        <td class="input" colspan="5"><textarea id="TextArea1" cols="20" rows="2" style="width:690px;"></textarea></td>
						    </tr>						    
					    </table>
				    </div>
				    
				    <!-- *********************************************************************
				    Mantenimiento de Vehículo
				         *********************************************************************
				    -->
                    <div id="dv_datos_vehiculo" class="dv_tabla_contenedora">
        					
					        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">                                                          
                              <tr>
                                    <td class="label">Marca</td>
                                    <td class="input"><input id="txtContrato3" type="text" class="css_input" size="15"/></td>
                                    <td class="label">Modelo</td>
                                    <td class="input"><input id="txtContrato4" type="text" class="css_input" size="15"/></td>
                                    <td colspan="2" rowspan="2">
                                        <fieldset>
                                            <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="label">Placa actual</td>
                                                <td class="input"><input id="txtContrato11" type="text" class="css_input" size="15"/></td>
                                            </tr>
                                            <tr>
                                                <td class="label">Placa anterior</td>
                                                <td class="input"><input id="txtContrato12" type="text" class="css_input" size="15"/></td>
                                            </tr>
                                            </table>
                                        </fieldset>
                                   </td>
                              </tr>
                              <tr>
                                <td class="label">N° Motor</td>
                                <td class="input"><input id="txtContrato1" type="text" class="css_input" size="15"/></td>
                                <td class="label">N° Serie</td>
                                <td class="input"><input id="txtContrato7" type="text" class="css_input" size="15"/></td>
                              </tr>
                              <tr>
                                <td class="label">Año fabricación</td>
                                <td class="input">
                                    <input id="Text4" type="text" class="css_input" size="15"/>
                                </td>
                                <td class="label">Color</td>
                                <td class="input"><input id="txtContrato8" type="text" class="css_input" size="15" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td class="label"> Ubicación</td>
                                <td class="input" colspan="5">
                                    <input id="txtContrato2" type="text" class="css_input" style="width:80%" />
                                </td>
                              </tr>
                              <tr>
                                <td class="label">Proveedor</td>
                                <td class="input">
                                    <input id="txtContrato10" type="text" class="css_input" size="15" />                                  
                                    <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor:pointer; vertical-align:middle;" />
                                </td>    
                                <td class="label">Razón social</td>
                                <td colspan="3" class="input"><input id="txtRazonSocialProveedor" type="text" size="80" readonly="readonly" disabled="disabled" /></td>
                              </tr>
                              <%--<tr>
                                <td colspan="6">
                                    <fieldset>
                                    <legend>Trámite de inmatriculación</legend>
                                                                            
                                        <table border="0" cellspacing="3" cellpadding="0" class="tb_formulario" style="padding-top:5px; width:100%;" >
                                          <tr>
                                            <td class="label" style="width:160px;">Fecha recepción documento</td>
                                            <td class="input" style="width:100px;">
                                              <input id="txtFechaIni0" type="text" class="css_input" size="11"/>
                                            </td>                                            
                                            <td class="label" style="text-align:left;">Observaciones</td>
                                          </tr>
                                          <tr>
                                            <td class="label">Fecha entrega documento</td>
                                            <td class="input">
                                              <input id="txtFechaIni" type="text" class="css_input" size="11"/>
                                            </td>                                            
                                            <td rowspan="2" class="input">
                                                <textarea name="textarea" id="textarea" cols="75" rows="5"></textarea>
                                            </td>
                                          </tr>
                                          <tr>
                                            <td class="label">Fecha recepción tarjeta prepago</td>
                                            <td class="input">
                                              <input id="txtFechaIni2" type="text" class="css_input" size="11"/>
                                            </td>
                                          </tr>
                                          <tr>
                                            <td class="label">N° Tarjeta propiedad</td>
                                            <td class="input">
                                              <input id="txtContrato9" type="text" class="css_input" size="15"/>
                                            </td>
                                          </tr>
                                        </table>
                                    </fieldset>
                              </td>
                              </tr>--%>
                            </table>
			        </div>
			        
			        <!-- *********************************************************************
				    Mantenimiento Otros
				         *********************************************************************
				    -->
			        <div id="dv_datos_otros" class="dv_tabla_contenedora">
    				    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0">                               
                            <tr>
                                <td class="label">Cantidad</td>
                                <td class="input"><input id="Text10" type="text" class="css_input" size="12" /></td>
                                <td class="label">Descripción</td>
                                <td colspan="3" class="input"><textarea name="textarea" id="textarea2" cols="55" rows="2" style="width:80%"></textarea></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                            <tr>
                                <td class="label">Ubicación</td>
                                <td class="input" colspan="3"><input id="Text11" type="text" class="css_input" style="width:80%" /></td>
                            </tr>
                            <tr>
                                <td class="label">Proveedor</td>
                                <td class="input">
                                    <input id="Text12" type="text" class="css_input" size="12" />&nbsp;&nbsp;
                                    <img src="../Util/images/ico_buscar.jpg" style="cursor:pointer; vertical-align:middle;" />
                                </td>
                                <td class="label">Razón Social</td>
                                <td colspan="3" class="input"><input id="Text13" type="text" style="width:100%;" readonly="readonly" disabled="disabled" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <fieldset>
                                    <legend>Datos adicionales</legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%; text-align:left;">
                                            <tr>
                                                <td class="label" style="text-align:left;">Observaciones</td>                                                        
                                                <td class="input"><textarea name="textarea" id="textarea3" cols="55" rows="4" style="width:100%"></textarea></td>
                                            </tr>
                                        </table>
                                    </fieldset>
                               </td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
			        
			    </td>
			</tr>
	    </table>
	    </div>	     


    </div>
    
    <asp:HiddenField ID="htipobien" runat="server" />
    <asp:HiddenField ID="hddCodigo" runat="server" />
    
</form>
 
</body>
</html>
