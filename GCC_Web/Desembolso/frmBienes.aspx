<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBienes.aspx.vb" Inherits="Desembolso_frmBienes" %>

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
	<script type='text/javascript' src="../Util/js/js_util.modal.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"></script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"></script>	
		
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->
    <script src="frmBienes.aspx.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="frmBienes" runat="server">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
	        <tr>
		        <td class="icono"><img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" /></td>
		        <td class="css_lbl_tituloContenido">
			        <div id="dvContrato">Desembolso</div>
                    <div id="dvRegistro">Desembolso :: Listado del Bien</div>
		        </td>
		        <td class="espacio">&nbsp;</td>
		        <td class="botones" style="width:300px;">
				    <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:parent.fn_util_CierraModal();">
					        <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
					        Cancelar
				        </a>
			        </div>
			        
				    <div id="Div1" class="dv_img_boton_separador">
	    		        :
    			    </div>
		
		    	    <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:parent.fn_util_CierraModal();">
					        <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
					        Guardar
				        </a>
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

		        </td>
	        </tr>
        </table>	
        
        
	    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
	    </table>	     
	          
        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titulo css_lbl_tituloContenido">Datos del Contrato</td>
            </tr>
        </table>
        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
            <tr>
                <td class="label" style="width:15%;">Nº Contrato</td>
                <td class="input" style="width:35%;">
                    <input id="Text9" type="text" class="css_input_inactivo" size="11" value="C0001" readonly="readonly" disabled="disabled" /></td>
                <td class="label" style="width:15%;">Clasificación del Bien</td>
                <td class="input" style="width:35%;"><input id="txtclasificacion" type="text" class="css_input_inactivo" size="55" value="BIENES INMUEBLES" readonly="readonly" disabled="disabled" /></td>
            </tr>
            <tr>
                <td class="label">CU Cliente</td>
                <td class="input"><input id="txtcu" type="text" class="css_input_inactivo" size="11" value="100285" readonly="readonly" disabled="disabled" /></td>
                <td class="label">Razón Social</td>
                <td class="input"><input id="Text11" type="text" class="css_input_inactivo" size="55" value="SERVICIOS LOGISTICOS S.A." readonly="readonly" disabled="disabled" /></td>
            </tr>
        </table>
                     
        <table id="Table1" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titulo css_lbl_tituloContenido">Datos del Bien</td>
            </tr>
        </table> 
        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
            <tr>
                <td class="label" style="width:15%;">Nº Bien</td>
                <td class="input" style="width:35%;">
                    <input id="Text1" type="text" class="css_input" size="11" value="0001" /></td>
                <td class="label" style="width:15%;">Tipo de Bien</td>
                <td class="input" style="width:35%;"><select name="ddlTipobien" id="ddlTipobien">
                                      <option selected="selected">- Seleccionar -</option>
                                      <option>Vehículos en General Excepto 4x4</option>
                                      <option>Camionetas 4x4</option>
                                      <option>Automóvil</option>
                                      <option>Remolcadores</option>
                                      <option>Camioneta</option>
                                      <option>Otro Veh. Liviano</option>
                                      <option>Remolcador</option>
                                      <option>Ómnibus</option>
                                      <option>Otro Veh. Pesado</option>
                                    </select></td>
            </tr>
        </table>

        <br />
        <table id="jqGrid_lista_E"><tr><td></td></tr></table> 
        <div id="jqGrid_pager_E"></div>  
            
        <asp:HiddenField ID="htipobien" runat="server" />
        <asp:HiddenField ID="hddCodigo" runat="server" />
        
    </form>
</body>
</html>
