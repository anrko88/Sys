<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTemporalRegistro.aspx.vb" Inherits="Temporal_frmTemporalRegistro" %>

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
	<script type="text/javascript" src="../Util/js/jquery/json2.js" ></script>
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>	
    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
    <script src="../Util/js/jquery/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>
    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>
    
    <!-- JQGrid -->		        
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmTemporalRegistro.aspx.js" > </script>
	
</head>
<body>
    
<form id="frmTemporalRegistro" runat="server">

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
				    <div class="css_lbl_subTitulo">Módulo Temporal</div>
				    <div class="css_lbl_titulo">Temporal :: Registro</div>
			    </td>
			    <td class="espacio">
			        &nbsp;    								
			    </td>
			    <td class="botones">    				
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_util_redirect('frmTemporalListado.aspx');">
						    <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" title="Cancelar" /><br />
						    Cancelar
					    </a>
				    </div>
    				
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_grabar();">
						    <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" title="Guardar" /><br />
						    Guardar
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
        				
				        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
					        <tr>
						        <td class="titulo css_lbl_tituloContenido">Datos de la Cotización</td>
						        <td class="botones">							
							        <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						        </td>
					        </tr>
				        </table >
				        
				        <div id="dv_datos"  class="dv_tabla_contenedora" >
      					
					         <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%;" >
					            <colgroup>                           
                                    <col style="width:220px" />
                                    <col style=""/>                                                       
                                    <col style="width:16.66%"/>                                                       
                                    <col style=""/>                                                                                                                                                    
                                </colgroup>
                                
						        <tr>
							        <td class="label">Fecha</td>
							        <td class="input">
						                <input id="txtFecha" type="text" class="css_input" size="12" value=""/>
						                <input type="button" id="btnBuscar" name="btnBuscar" value="Buscar ASHX" class="css_btn_general" onclick="javascript:fn_buscarDatosASHX();" />
						                <input type="button" id="btnBuscarWM" name="btnBuscarWM" value="Buscar WebMethod" class="css_btn_general" onclick="javascript:fn_buscarDatosWM();" />
							        </td>
                                </tr>    							        
							    <tr>
							        <td class="label">Numero</td>
						            <td class="input"><input id="txtNumero" type="text" class="css_input" size="30" value=""/></td>						            
						        </tr>
						        <tr>
						          <td class="label" >Combo 1</td>
						          <td class="input"><select name="ddlCombo" id="ddlCombo" >
						            <option value="0">- Seleccionar -</option>
						            <option value="1">Dato 1</option>
						            <option value="2">Dato 2</option>
					              </select></td>
                                </tr>
						        <tr>
						            <td class="label" >Combo 2</td>								    							        
							        <td class="input"><select name="ddlCombo2" id="ddlCombo1" >
							          <option value="0">- Seleccionar -</option>
						            </select></td>
							    </tr>
						        <tr>
						          <td class="label" >Radio
					              </td>
						          <td class="input">
                                  <span class="label">
						            <input type="radio" name="radio" id="radio1" value="0" />
                                    <label for="radio1">Opción 0</label>
					                <input type="radio" name="radio" id="radio2" value="1" />
						            <label for="radio2">Opción 1</label>
						          </span></td>
                                </tr>	
						        	
						        <tr>
						            <td class="label">Texto</td>
						            <td class="input"><input id="txtTexto" type="text" class="css_input" size="30" value=""/></td>						     
							    </tr>
							    
							    <tr>
						            <td class="label">Flag</td>
						            <td class="input"><input id="chkFlag" type="checkbox" class="css_input" value="1"/></td>						     
							    </tr>
							    					    
						        <tr>
						            <td class="label">Decimales</td>
						            <td class="input"><input id="txtDecimales" type="text" class="css_input" size="30" value=""/></td>						     
							    </tr>							    
							    
							    <tr>
						            <td class="label">Comentario</td>
						            <td class="input"><textarea id="txaComentario" class="css_input" cols="50" rows="3" ></textarea></td>						     
							    </tr>							    
							    
                            </table>
                            
			           </div>
        		    </td>
    			</tr>
    	</table>	
        </div>
    </div>  
    
    <input id="hddCodigo" type="hidden" runat="server" />
          
</form>

</body>
</html>