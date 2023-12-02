<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSeguroListado.aspx.vb" Inherits="Mantenimiento_SeguroListado" %>

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
    <form id="frmSeguroListado" runat="server">

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
				    <div class="css_lbl_subTitulo">Mantenimiento</div>
				    <div class="css_lbl_titulo">Mantenimiento :: Seguro</div>
			    </td>
			    <td class="espacio">
			        &nbsp;    								
			    </td>
			    <td class="botones">
    			
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
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_abreDetalle('')">
						    <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" title="Editar" /><br />
						    Editar
					    </a>
				    </div>				    				     
			    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_util_redirect('frmSeguroMant.aspx');">
						    <img alt="" src="../Util/images/ico_acc_agregar.gif" border="0" title="Agregar" /><br />
						    Agregar
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
							    <td class="titulo css_lbl_tituloContenido">Datos para la búsqueda</td>
							    <td class="botones">
								    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
							    </td>
						    </tr>
					    </table>
    					
					    <div class="dv_tabla_contenedora">
    						
						    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
							    <tr>
							        <td class="label">N° Contrato</td>
							        <td class="input"><input id="txtContrato" type="text" class="css_input" size="20"/></td>
								    <td class="label">CU Cliente</td>
							        <td class="input"><input id="txtContrato0" type="text" class="css_input" size="20"/></td>
							        <td class="label">Razón Social o Nombre</td>
							        <td class="input"><input id="txtrazonsocial0" type="text" class="css_input" runat="server" size="55" /></td>
							    </tr>
							    <tr>
							        <td class="label">N° Póliza</td>
							        <td class="input"><input id="Text1" type="text" class="css_input" size="20"/></td>
								    <td class="label">Tipo de Valor</td>
							        <td class="input">
							            <select id="cmbTipoValor" class="">
									        <option value="0">- Seleccionar -</option>
                                            <option value="1">Tasación</option>
                                            <option value="2">Reposición</option>
                                            <option value="3">Comercial</option>
                                            <option value="4">Convenido</option>
                                            <option value="5">Reemplazo</option>
                                            <option value="6">Total</option>
                                            <option value="7">Factura</option>
								        </select>
							        </td>			
							         <td class="label">Fecha Inicio Póliza</td>
							        <td class="input"><input id="txtFechaIni" type="text" class="css_input" size="11"/></td>
							    </tr>
							    <tr>
							        <td class="label">Tipo  de Bien</td>
							        <td class="input"><input id="Text2" type="text" class="css_input" size="50"/></td>
							        <td class="label">Tipo de Seguro</td>
							        <td class="input">
							            <select id="cmbTipoSeguro" class="">
									        <option>- Seleccionar -</option>
                                            <option>Clientes con pólizas externas pendientes</option>
                                            <option>Cotización</option>
                                            <option>Excepción</option>
                                            <option>Seguro del Banco</option>
                                            <option>Seguro Particular</option>
                                            <option>Sin responsabilidad</option>
								        </select>
								    </td>
								    <td class="label">Fecha Fin Póliza</td>
							        <td class="input"><input id="txtFechaFin" type="text" class="css_input" size="11"/></td>
							    </tr>
						    </table>
    						
					    </div>
    					
					    <br />
    					
					    <div class="dv_tabla_contenedora">
						    <table id="jqGrid_lista_B"><tr><td/></tr></table> 
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
