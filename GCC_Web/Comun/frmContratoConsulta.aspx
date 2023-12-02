<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmContratoConsulta.aspx.vb" Inherits="Comun_frmContratoConsulta" %>

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
	<script type='text/javascript' src="frmContratoConsulta.aspx.js"></script>
	
</head>
<body>
    <form id="frmContratoConsulta" runat="server">

    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
    	<!-- **************************************************************************************** -->
        <!-- BOTONES -->
        <!-- **************************************************************************************** -->
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
		    <tr>
		        <td class="icono"><img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" /></td>
		        <td class="css_lbl_tituloContenido">
			        <div id="dvContrato">Gestión del Bien</div>
                    <div id="dvRegistro">Gestión del Bien :: Listado de Contrato</div>
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
			    </td>
		    </tr>
	    </table>
    		
	    <div id="dv_contenedor" class="css_scrollPane">
	        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
		        <tr>
			        <td class="lineas"></td>			
		        </tr>		        
		        <tr>
			        <td>
		                <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">Datos del Contrato</td>
                            </tr>
                        </table>
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="label" style="width:16.66%;">Nº Contrato</td>
                                <td class="input" style="width:16.66%;">
                                    <input id="Text9" type="text" class="css_input" size="11" value="" /></td>
                                <td class="label" style="width:16.66%;">CU Cliente</td>
                                <td class="input" style="width:16.66%;">
                                    <input id="Text12" type="text" class="css_input" size="11" value="" /></td>
                                <td class="label" style="width:16.66%;">Razón Social</td>
                                <td class="input" style="width:16.66%;"><input id="Text11" type="text" class="css_input" size="55" value="SERVICIOS LOGISTICOS S.A." /></td>
                            </tr>
                            <tr>
                                <td class="label">Ejecutivo</td>
                                <td class="input"><select id="ddlEjecutivo">
                                            <option selected="selected">- Seleccionar -</option>
                                            <option>Bruno de la Cruz</option>                                                                                  
                                            <option>Javier Choy Ortiz</option>                                                                                  
                                            <option>Jorge Tuesta Lujan</option>                                                                            
                                            <option>Gino Purín Subiría</option>                                                                                
                                            <option>Percy Chavez Castillo</option>                                                                              
                                            <option>José Antonio Cano Revilla</option>                                                                          
                                            <option>Gonzalo Marrero Boisset</option>                                                                            
                                            <option>Claudio Thiermann</option>                                                                                
                                            <option>Ninguno</option>                                                                                         
                                            <option>José García León</option>                                                                                  
                                            <option>Brian Coleridge Garcia</option>  
                                        </select></td>
                                <td class="label">Estado</td>
                                <td class="input"><select id="ddlEstado">
							            <option selected="selected">- Seleccionar -</option>
                                        <option>Vigente</option>
                                        <option>Anulado</option>
                                        <option>Resuelto</option>
                                        <option>Judicial</option>
                                        <option>Cancelado</option>
                                        <option>Cerrado</option>
                                    </select></td>
                                <td class="label">Clasificación del Contrato</td>
                                <td class="input"><select id="Select1">
				                    <option value="0" selected="selected">- Seleccionar -</option>
                                    <option value="2">Inmuebles</option>
                                    <option value="3">Mueble</option>
                                    <option value="4">Vehículo</option>
                                    <option value="7">Embarcación pesquera</option>
                                </select></td>
                            </tr>
                        </table>
			        </td>
		        </tr>
	        </table>

            <br />
		    <table id="jqGrid_lista_C"></table>
		    <div id="jqGrid_pager_C"></div>
	    </div>
    	
    </div>

    <asp:HiddenField runat="server" ID="hddCodigoContrato" Value="" />

    </form>
</body>
</html>