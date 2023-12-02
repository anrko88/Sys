<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRepresentanteRegistroProv.aspx.vb" Inherits="Comun_frmRepresentanteRegistroProv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro Representante</title>
    
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
	<script type='text/javascript' src="frmRepresentanteRegistroProv.aspx.js"> </script>
	
</head>
<body>
    <form id="frmObservacion" runat="server">
     <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
	        <tr>
			                <td class="icono">
				                <img src="../Util/images/ico_mdl_multa.gif" class="jd_menu_icono"/>		
			                </td>
			                <td class="titulos">
				                <div class="css_lbl_subTitulo">Representantes Provincia</div>
				                <div class="css_lbl_titulo">Representante :: Nuevo</div>
			                </td>
			                <td class="espacio">&nbsp;
                								
			                </td>
			                <td class="botones">
                			
    				            <div id="dv_img_boton" class="dv_img_boton">
					                <a href="javascript:parent.fn_util_CierraModal();">
						                <img src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						                Cancelar
					                </a>
				                </div>
            				    
				                <div id="Div3" class="dv_img_boton_separador">
				                    :
				                </div>
            				    
				                <div id="dv_img_boton" class="dv_img_boton">
					                <a href="javascript:parent.fn_util_CierraModal();">
						                <img src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
						                Guardar
					                </a>
				                </div>	
				                
				                <div id="Div1" class="dv_img_boton" style="width:75px;">
					                <a href="javascript:parent.fn_util_CierraModal();">
						                <img src="../Util/images/ok.gif" border="0" width="35"/><br />
						                Seleccionar
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
				    
				    <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%" >
					    <tr>
						    <td class="titulo css_lbl_tituloContenido">Nuevo Representante</td>
					    </tr>
				    </table>
				    <div class="dv_tabla_contenedora">
				        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%;" >
				            <tr>
                                <td class="label">Departamento</td>
                                <td class="input">
                                    <select id="cmbDepartamento" name="cmbDistrito">
                                        <option value="0">- Seleccionar -</option>                                                    
                                    </select>
                                </td>
                                <td class="label">Provincia</td>
                                <td class="input">
                                    <select id="cmbProvincia" name="cmbDistrito">
                                        <option value="0">- Seleccionar -</option>                                                    
                                    </select>    
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Distrito</td>
                                <td class="input">
                                    <select id="cmbDistrito" name="cmbDistrito">
                                        <option value="0">- Seleccionar -</option>                                                    
                                    </select>  
                                </td>                                                            
                            </tr>
                            <tr>
                                <td class="label">DNI</td>
                                <td class="input"><input id="txtDni" name="txtDni" type="text" class="css_input" size="14" value="" /></td>
                                <td class="label">Representante</td>
                                <td class="input"><input id="txtRepresentante" name="txtRepresentante" type="text" class="css_input" size="40" value="" /></td>
                            </tr>
                            <tr>
                                <td class="label">Partida Registral</td>
                                <td class="input"><input id="txtPartidaRegistral" name="txtPartidaRegistral" type="text" class="css_input" size="14" value="" /></td>
                                <td class="label">Oficina Registral</td>
                                <td class="input"><input id="txtOficinaRegistral" name="txtOficinaRegistral" type="text" class="css_input" size="20" value="" /></td>
                            </tr>                 
                        </table>
				    </div>
				    
				    <br />
				    <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%" >
					    <tr>
						    <td class="titulo css_lbl_tituloContenido">Listado de Representantes</td>
					    </tr>
				    </table>
				    <div class="dv_tabla_contenedora">
				        <table style="width:100%;"> 
				            <tr>  
                                <td>
                                    <table id="jqGrid_lista_A"><tr><td/></tr></table> 
                                </td>
                            </tr>	
                        </table>
				    </div>
				    
                </td>
            </tr>
	    </table>		    
  	            
    </div>
    
    <asp:HiddenField ID="hdnTitulo" runat="server" />
    <asp:HiddenField ID="hflagtipoObs" runat="server" />
    </form>
</body>
</html>
