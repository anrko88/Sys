<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAsociaDocDetalle.aspx.vb" Inherits="GestionBien_frmAsociaDocDetalle" %>

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
	<script type='text/javascript' src="frmAsociaDocDetalle.aspx.js"> </script>
</head>
<body>
    
<form id="frmAsociaDocDetalle" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    	    <tr>
			    <td class="icono">&nbsp;</td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo">Gestión del Bien</div>
				    <div class="css_lbl_titulo">Asociación :: Documentos</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
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
					        <a href="javascript:fn_guardar();">
						        <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
						        Guardar
					        </a>
				        </div>

			    </td>
		    </tr>
	    </table>	
	    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
		    <tr>
			    <td class="cuerpo">
		            <table id="tb_tabla_comunCabecera1" border="0" cellpadding="0" cellspacing="0">
					    <tr>
						    <td class="titulo css_lbl_tituloContenido">Documentos</td>
					    </tr>
				    </table>
				    
				    <div class="dv_tabla_contenedora"> 
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="label" >Nº Contrato</td>
                                <td class="input">
                                    <input name="txtNroCotizacion" type="text" id="txtNroCotizacion" class="css_input_inactivo" value="121354" size="25" readonly="readonly" />
                                </td>       
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr> 
	                        <tr>
		                        <td class="label">Clasificación del bien</td>
		                        <td class="input">
		                            <input name="txtTotal" type="text" id="Text1" class="css_input_inactivo" value="Unidad de transporte terrestre" size="25" readonly="readonly" />
	                            </td>
		                        <td class="label">Tipo del bien</td>
		                        <td class="input">
		                            <input name="txtTotal" type="text" id="Text2" class="css_input_inactivo" value="Automovil" size="25" readonly="readonly" />
	                            </td>
	                        </tr>  
	                        <tr>
		                        <td class="label">Descripción</td>
		                        <td class="input"><input name="txtTotal" type="text" id="Text3" class="css_input_inactivo" value="Mazda" size="25" readonly="readonly" /></td>
		                        <td class="label">Valor Total</td>
		                        <td class="input"><input name="txtTotal" type="text" id="Text4" class="css_input_inactivo" value="21,660.00" size="25" readonly="readonly" /></td>
	                        </tr>   	
                        </table> 
                 
                        <br />
                 						
                        <table id="jqGrid_lista_B"><tr><td/></tr></table> 
                        <div id="jqGrid_pager_B"></div>
                        
                        <table width="100%">
                            <tr align ="right">
		                        <td>
			                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:250px;"> 
				                        <tr align ="right" >
					                        <td class="label" colspan="2">TOTAL&nbsp&nbsp&nbsp</td>
					                        <td ><input name="txtTotal" type="text" id="txtTotal" class="css_input" value="43,000.00" size="13" style="text-align:right;" /></td>
				                        </tr>   	
			                        </table> 
		                        </td>
	                        </tr>
                        </table>
                        
                    </div>
            	    
				    
			    </td>
		    </tr>
	    </table>		

       
    <!-- Fin Cuerpo -->	
    </div>
</form>
</body>
</html>
