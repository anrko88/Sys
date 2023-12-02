<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAsociarBien.aspx.vb" Inherits="Desembolso_frmAsociarBien" %>

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
    <script src="frmAsociarBien.aspx.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="frmAsociarBien" runat="server">
    
     <!-- **************************************************************************************** -->
     <!-- CUERPO -->
     <!-- **************************************************************************************** -->
     <div id="dv_cuerpo">     
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" />		
                </td>
            <td class="titulos">
	            <div class="css_lbl_subTitulo">Desembolso</div>
	            <div class="css_lbl_titulo">Desembolso: Modificación Bien</div>
            </td>
            <td class="espacio">&nbsp;</td>
            <td class="botones">
		        <div id="dv_img_boton" class="dv_img_boton">
		           <a href="javascript:parent.fn_util_CierraModal();">
		            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
			            Cancelar
		            </a>
	            </div>
		        <div id="dv_img_boton" class="dv_img_boton">
		            <a href="javascript:fn_grabar();">
			        <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
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
	     
        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
	        <tr>
		        <td class="titulo css_lbl_tituloContenido">
		            <div id="dvContrato">Nro Contrato:&nbsp;&nbsp;0003</div>
		            <div id="dvRegistro">Registro</div>
		        </td>
	        </tr>
        </table>
    </div>
    
    <br />
    <table id="jqGrid_lista_E"><tr><td></td></tr></table> 
    <div id="jqGrid_pager_E"></div>
        
    <asp:HiddenField ID="htipobien" runat="server" />
    <asp:HiddenField ID="hddCodigo" runat="server" />
    
    </form>
</body>
</html>
