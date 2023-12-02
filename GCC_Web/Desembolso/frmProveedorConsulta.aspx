<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProveedorConsulta.aspx.vb" Inherits="GestionBien_Desembolso_frmProveedorConsulta" %>

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
	<script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"></script>		
	<script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"></script>
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"></script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"></script>		

	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmProveedorConsulta.aspx.js"></script>
	
</head>
<body>
    <form id="frmProveedorConsulta" runat="server">

    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
    	
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="icono">
				    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo">Desembolso</div>
				    <div class="css_lbl_titulo">Desembolso :: Proveedor</div>
			    </td>
			    <td class="espacio">
			        &nbsp;    								
			    </td>
			    <td class="botones">
    			    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:parent.fn_util_CierraModal();">
						    <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
						    Cancelar
					    </a>
				    </div>
				    
				    <div id="Div3" class="dv_img_boton_separador">
	    		        :
    			    </div>
    			    
    				<div id="dv_img_boton" class="dv_img_boton">
					    <a href="#">
						    <img alt="" src="../Util/images/ico_acc_buscar.gif" border="0" title="Buscar" /><br />
						    Buscar
					    </a>
				    </div>	
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="#">
						    <img alt="" src="../Util/images/ico_acc_limpiar.gif" border="0" title="Limpiar" /><br />
						    Limpiar
					    </a>
				    </div>
				    
				    <div id="dv_img_boton" class="dv_img_boton" style="width:95px;">
					    <a href="javascript:fn_AgregaProveedor();">
						    <img alt="" src="../Util/images/ico_acc_agregarProveedor.gif" border="0" title="Limpiar" /><br />
						    Agregar Proveedor
					    </a>
				    </div>
    				
			    </td>
		    </tr>
	    </table>	
    	

	    <div id="dv_contenedor" class="css_scrollPane">	
		    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
			    <tr>
				    <td class="lineas"></td>			
			    </tr>
			    <tr>
				    <td class="cuerpo">
    					
					    <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
						    <tr>
							    <td class="titulo css_lbl_tituloContenido">Datos de búsqueda</td>
						    </tr>
					    </table>
    					
					    <div class="dv_tabla_contenedora">
    						
						    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
							    <tr>
							        <td class="label">
							            RUC</td>
							        <td class="input">
							            <input id="txtContrato" type="text" class="css_input" size="12" />
							        </td>
								    <td class="label">
							            Razón Social</td>
							        <td class="input">
									    <input id="txtrazonsocial" type="text" class="css_input" runat="server" size="75" /></td>
							    </tr>
							    </table>
    						
					    </div>
    					
					    <br />    					
					    <div class="dv_tabla_contenedora">
						    <table id="jqGrid_lista_A"><tr><td /></tr></table> 
						    <div id="jqGrid_pager_A"></div> 
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
