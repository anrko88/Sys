<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSeguroMant.aspx.vb" Inherits="Mantenimiento_frmSeguroMant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmSeguroListado.aspx.js"> </script>
</head>
<body>
    <form id="frmSeguroMant" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
    	
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="icono">
				    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono"/>		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo">Administración</div>
				    <div class="css_lbl_titulo">Administración :: Seguro - Nuevo</div>
			    </td>
			    <td class="espacio">
			        &nbsp;    								
			    </td>
			    <td class="botones">
    				
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_util_redirect('frmProveedorListado.aspx');">
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
    		
	    <div id="dv_contenedor" class="css_scrollPane">			
	        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
		        <tr>
			        <td class="lineas">
			            &nbsp;
			        </td>			
		        </tr>
		        <tr>
			        <td class="cuerpo">
        				
				        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
					        <tr>
						        <td class="titulo css_lbl_tituloContenido">Datos del Contrato</td>
						        <td class="botones">							
							        <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						        </td>
					        </tr>
				        </table>        				
				        <div id="dv_datos" class="dv_tabla_contenedora">
        					
					        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0">
						        <tr>
							        <td class="label">Nº Contrato</td>
							        <td class="input"><input id="Text11" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">CU Cliente</td>
							        <td class="input"><input id="Text12" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Razón Social o Nombre</td>
							        <td class="input"><input id="Text13" type="text" class="css_input" size="50"/></td>					    
						        </tr>						    
						        <tr>	
                                    <td class="label">Tipo de Documento</td>
							        <td class="input"><input id="Text14" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Nº de Documento</td>
							        <td class="input"><input id="Text15" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Clasificación del Bien</td>
							        <td class="input"><input id="Text16" type="text" class="css_input" size="50"/></td>					    
						        </tr>	
						        <tr>	
							        <td class="label">Moneda</td>
							        <td class="input"><input id="Text17" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Tipo de Seguro</td>
							        <td class="input"><input id="Text18" type="text" class="css_input" size="15"/></td>					    
							        <td>&nbsp;</td>
							        <td>&nbsp;</td>
							    </tr>	
					        </table>
				        </div>
        				<table id="Table1" border="0" cellpadding="0" cellspacing="0">
					        <tr>
						        <td class="titulo css_lbl_tituloContenido">Datos de la Póliza</td>
						        <td class="botones">							
							        <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						        </td>
					        </tr>
				        </table> 
				        <div id="tb_formulario" class="dv_tabla_contenedora">
					        <table id="Table2" border="0" cellpadding="0" cellspacing="0">
						        <tr>	
                                    <td class="label">Nº Póliza</td>
							        <td class="input"><input id="Text1" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Tipo de Seguro</td>
							        <td class="input"><input id="Text2" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">CIA. Seguro</td>
							        <td class="input"><input id="Text3" type="text" class="css_input" size="50"/></td>					    
						        </tr>
						        <tr>	
                                    <td class="label">Tipo del Bien</td>
							        <td class="input"><input id="Text4" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Tipo Valor</td>
							        <td class="input"><input id="Text5" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Tipo Declaración</td>
							        <td class="input"><input id="Text19" type="text" class="css_input" size="50"/></td>					    
						        </tr>
						        <tr>	
                                    <td class="label">Fecha Inicio Crédito</td>
							        <td class="input"><input id="Text20" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Fecha Fin Crédito</td>
							        <td class="input"><input id="Text21" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Ramo</td>
							        <td class="input"><input id="Text22" type="text" class="css_input" size="50"/></td>					    
						        </tr>
						        <tr>	
                                    <td class="label">Fecha Inicio Póliza</td>
							        <td class="input"><input id="Text23" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Fecha Fin Póliza</td>
							        <td class="input"><input id="Text24" type="text" class="css_input" size="15"/></td>					    
							        <td>&nbsp;</td>
							        <td>&nbsp;</td>
							    </tr>
							    <tr>	
                                    <td class="label">Valor Prenda</td>
							        <td class="input"><input id="Text25" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Valor Endoso</td>
							        <td class="input"><input id="Text26" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Prima Neta</td>
							        <td class="input"><input id="Text27" type="text" class="css_input" size="50"/></td>					    
						        </tr>
					        </table>
				        </div>
				        <table id="Table3" border="0" cellpadding="0" cellspacing="0">
					        <tr>
						        <td class="titulo css_lbl_tituloContenido">Datos de la Póliza</td>
						        <td class="botones">							
							        <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						        </td>
					        </tr>
				        </table> 
				        <div id="tb_formulario" class="dv_tabla_contenedora">
        					
					        <table id="Table4" border="0" cellpadding="0" cellspacing="0">
				                 <tr>	
                                    <td class="label">Codigo Movimiento</td>
							        <td class="input"><input id="Text6" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Descripción Movimiento</td>
							        <td class="input"><input id="Text7" type="text" class="css_input" size="15"/></td>					    
							        <td>&nbsp;</td>
							        <td>&nbsp;</td>
							    </tr>
							    <tr>	
                                    <td class="label">Nº Carta</td>
							        <td class="input"><input id="Text8" type="text" class="css_input" size="15"/></td>					    
							        <td class="label">Fecha de Carta</td>
							        <td class="input"><input id="Text9" type="text" class="css_input" size="15"/></td>					    
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
    </form>
</body>
</html>
