<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCobroCobrar.aspx.vb" Inherits="Comun_frmCobroCobrar" %>

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
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmCobroCobrar.aspx.js"></script>
	
</head>
<body>
 
<form id="frmCobroCobrar" runat="server">

    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
    	
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
		    <tr>
			    <td class="icono">
				    <img alt="" src="../Util/images/ico_acc_cobro.gif" class="jd_menu_icono" />		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo"></div>
				    <div class="css_lbl_titulo" id="dvTitulo">Cobro</div>
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
					    <a href="javascript:parent.fn_util_CierraModal();">
						    <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
						    Guardar
					    </a>
				    </div>
    				
			    </td>
		    </tr>
	    </table>	
            
	    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
		    <tr>
			    <td class="cuerpo">
			    
				    <div class="dv_tabla_contenedora">
					    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">   
                            <tr>  
                                <td class="label" style="width:15%;">Moneda</td>
       					        <td class="input" style="width:35%;"><select name="Select" id="Select3" class="css_input">
		                                <option selected="selected">- Seleccionar -</option>
		                                <option>Nuevos Soles</option>
		                                <option>Dólares Americanos</option>
	                                  </select></td>
                                <td class="label" style="width:15%;">Fecha de Cobro</td>
       					        <td class="input" style="width:35%;">
                                    <input id="txtFechaini0" type="text" class="css_input" size="12" 
                                        title="Fecha inicial de búsqueda del pago del impuesto" /></td>
   					        </tr>
                            <tr>  
                                <td class="label" style="width:15%;">Tipo de Cambio</td>
       					        <td class="input" style="width:35%;"><input id="Text2" type="text" class="css_input" size="20" style="text-align:right;" /></td>
                                <td class="label" style="width:15%;">Carta Individual</td>
       					        <td class="input" style="width:35%;">
                                    <input id="Checkbox1" type="checkbox" class="css_input" /></td>
   					        </tr>
						</table>
						    
						<br />					
		                <table id="tb_tabla_comunCabecera1" border="0" cellpadding="0" cellspacing="0">
					        <tr>
						        <td class="titulo css_lbl_tituloContenido">&nbsp;</td>
					        </tr>
				        </table>		       						
				        <table id="jqGrid_lista_B"><tr><td /></tr></table> 
				        <div id="jqGrid_pager_B"></div>
			        </div>
				    
		        </td>
		    </tr>
	    </table>    

    <!-- Fin Cuerpo -->	
    </div>    	

    <asp:HiddenField runat="server" ID="hddGestion" Value="" />
    <asp:HiddenField runat="server" ID="hddCodigoContrato" Value="" />

</form>

</body>
</html>
