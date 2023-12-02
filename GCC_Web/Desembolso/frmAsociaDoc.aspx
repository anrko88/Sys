<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAsociaDoc.aspx.vb" Inherits="Desembolso_frmAsociaDoc" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>GCC :: Sistema de Gestión de Leasing</title>
	
	<!-- Icono URL -->
	<link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
	
	<!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css" media="all" /><link type="text/css" rel="stylesheet" href="../Util/css/css_global.css" /><link type="text/css" rel="stylesheet" href="../Util/css/css_formulario.css" /><link type="text/css" rel="stylesheet" href="../Util/css/css_fuente.css" />
	
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
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmAsociaDoc.aspx.js"> </script>
	
</head>
<body>
    
<form name="frmVersionesListado" runat="server"> 
 
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
			    <td class="espacio">&nbsp;
    								
			    </td>
			    <td class="botones">
    				
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_util_redirect('frmDesembolsoListado.aspx');">
						    <img src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						    Cancelar
					    </a>
				    </div>
    				
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_grabar();">
						    <img src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
						    Grabar
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
				    <td class="cuerpo">
 
    					<div id="divTabs" style="border:0px; background:none;" >
					        <ul>
						        <li><a href="#tab-0">Doc. Desembolso</a></li>
						        <li><a href="#tab-1">Asocia Doc.</a></li>
					        </ul>
					        					        
					        <div id="tab-0">        				
        					    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:700px;">
					                <tr>
						                <td class="label" >Nº Contrato</td>
						                <td class="input"><input id="txtnumcontrato" type="text" class="css_input" size="11" value="C0001" /></td>
						                <td class="label">Fecha</td>
						                <td class="input"><input id="txtfechaini" type="text" class="css_input" size="11"  /></td>
						                <td></td>
						                <td></td>

						            </tr>
						            <tr>
						                <td class="label">Procedencia</td>
						                <td class="input">
                                            <select id="Select1">
                                                <option>Normal</option>
                                                <option>Extranjera</option>
                                            </select>
                                        </td>
						                <td class="label">Ruc</td>
						                <td class="input"><input id="txtcliente" type="text" class="css_input" size="11" value=""/></td>
						                <td class="label">Proveedor</td>
						                <td class="input"><input id="Text1" type="text" class="css_input" size="20" value=""/></td>
						            </tr>
						            <tr>    
						                <td class="label">F. Emisión</td>
						                <td class="input"><input id="txtfechaemision" type="text" class="css_input" size="10" value=""/></td>
						                <td class="label">Tipo Documento</td>
						                <td class="input">
						                    <select id="Select2">
                                                <option>Factura</option>
                                                <option>Boleta</option>
                                                <option>DUA</option>
                                            </select>
						                </td>
						                <td class="label">Nº Documento</td>
						                <td class="input"><input id="Text2" type="text" class="css_input" size="10" value=""/></td>
						            </tr>
						            <tr>
						                <td class="label">Descripcion</td>
						                <td colspan="6">
                                            <textarea id="TextArea1" cols="20" rows="2"></textarea>
						                </td>
                                    </tr>
    					            <tr>
						                <td class="label">Moneda</td>
						                <td class="input">
						                    <select id="Select3">
                                                <option>Soles</option>
                                                <option>Dolares</option>
                                            </select>
						                </td>
						                <td class="label">Valor Venta</td>
						                <td class="input">
						                    <select id="Select4">
                                                <option>Soles</option>
                                                <option>Dolares</option>
                                            </select>
						                </td>
                                        <td class="label" style="width: 130px">T.C. del Dia</td>
						                <td class="input"><input id="Text3" type="text" class="css_input" size="10" value=""/></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="label">IGV</td>
                                        <td class="input"><input id="Text4" type="text" class="css_input" size="10" value=""/></td>
                                        <td class="label" style="width: 130px">T.C. Especial</td>
                                        <td class="input"><input id="Text5" type="text" class="css_input" size="10" value=""/></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="label">Total</td>
                                        <td class="input"><input id="Text6" type="text" class="css_input" size="10" value=""/></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">Retención</td>
                                        <td>
                                            <input id="Checkbox1" type="checkbox" />
                                        </td>
                                        <td></td>
                                        <td class="input" style="width: 130px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">Detracción</td>
                                        <td>
                                            <input id="Checkbox2" type="checkbox" />
                                        </td>    
                                        <td>
						                    <select id="Select5" name="D1">
                                                <option>Servicios </option>
                                                <option>Transporte</option>
                                            </select>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">	
                                            <br />		
					                        <table id="jqGrid_lista_C"><tr><td/></tr></table> 
					                        <div id="jqGrid_pager_C"></div>				                        
                                        </td>
                                    </tr>
    				           </table >  
					        </div>
					        
					        <div id="tab-1">    						
						        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                    <tr>                                        
                                        <td class="label" style="width:50px;" >Nº Contrato</td>
                                        <td class="input"><input name="txtNroCotizacion" type="text" id="txtNroCotizacion" class="css_input" /></td>                                                             
                                    </tr>   	
									<tr>
										<td colspan="3" class="titulo css_lbl_tituloContenido">Bienes</td>	
									</tr>									
									<tr>
										<td colspan="3">
											<table id="jqGrid_lista_A"></table> 
											<div id="jqGrid_pager_A"></div> 
										</td>
									</tr>
                                </table>					            
					        </div>
					        
				     </div> 
					    					                					
				    </td>
			    </tr>
 
		    </table>
	    </div>
 
    <!-- Fin Cuerpo -->	
    </div>
    
</form>
 
</body>
</html>

