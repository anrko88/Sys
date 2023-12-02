<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCheckListLegalListado.aspx.vb" Inherits="Formalizacion_frmCheckListLegalListado" %>

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
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>	
	<script type="text/javascript" src="../Util/js/jquery/jquery.dateFormat-1.0.js"></script>
    <script type="text/javascript" src="../Util/js/jquery/jshashtable.js"></script>    
    <script type="text/javascript" src="../Util/js/js_util.Grilla.js"></script>

	<!-- JQGrid -->
	<script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmCheckListLegalListado.aspx.js"> </script>
	
</head>
<body>
    
<form id="frmChecklistListado" runat="server">

    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
    	
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="icono">
				    <img alt="" src="../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono"/>		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo" id="lbl_SubTitulo">Formalización</div>
				    <div class="css_lbl_titulo" id="lbl_titulo">Checklist Legal :: Listado</div>
			    </td>
			    <td class="espacio">
			        &nbsp;    								
			    </td>
			    <td class="botones">				  			
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_BuscarContratoCotizacion(true);">
						    <img alt="" src="../Util/images/ico_acc_buscar.gif" border="0"/><br />
						    Buscar
					    </a>
				    </div>	
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_Limpiar();">
						    <img alt="" src="../Util/images/ico_acc_limpiar.gif" border="0"/><br />
						    Limpiar
					    </a>
				    </div>	
    				
				    <div id="dv_separador" class="dv_img_boton_separador">
				        :
				    </div>
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_Editar();">
						    <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" title="Editar" /><br />
						    Editar
					    </a>
				    </div>
				        					    				
			    </td>
		    </tr>
	    </table>	
    	
	    <div id="dv_contenedor"  >
	        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
		        <tr>
			        <td class="lineas"></td>			
		        </tr>
		        <tr>
			        <td class="cuerpo">
        				
				        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
					        <tr>
						        <td class="titulo css_lbl_tituloContenido">
						            Datos de Búsqueda
					            </td>
						        <td class="botones">							
							        <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
						        </td>
					        </tr>
				        </table>
        				    				
				        <div id="dv_datos" class="dv_tabla_contenedora">
        					
					        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" >
                                <tr>
                                    <td class="label">Nº Cotización</td>
                                    <td class="input">
                                        <input id="txtNroCotizacion" name="txtNroCotizacion"  type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">CU Cliente</td>
                                    <td class="input">
                                        <input id="txtCuCliente" name="txtCuCliente" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">Razón Social o Nombre</td>
                                    <td class="input">
                                        <input id="txtRazonSocial" name="txtRazonSocial" type="text" class="css_input" size="65" runat="server" />
                                    </td>
                                </tr>   								
                                <tr> 
                                    <td class="label">N° Contrato</td>
                                    <td class="input"><input id="txtContrato" name="txtContrato" type="text" class="css_input" runat="server" /></td>  
                                    <td class="label">Ejecutivo</td>
                                    <td class="input">
                                        <select id="cmbEjecutivo" name="cmbEjecutivo"  runat="server" >
                                        </select>
                                    </td>
                                    <td class="label">Fecha Inicio</td>
                                    <td class="input">
                                        <input id="txtFechaIni" name="txtFechaIni" type="text" class="css_input" size="11"  runat="server"  />
                                    </td>
                                </tr>	
                                <tr>
                                    <td class="label">Clasificación del Bien</td>
                                    <td class="input">
                                            <select id="cmbClasificacionContrato" name="cmbClasificacionContrato"  runat="server" >
                                            </select>
                                    </td> 
                                     <td class="label"> Banca en Atención</td>
                                     <td class="input">
                                         <select id="cmbZonal" name="cmbZonal" runat="server">
                                         </select>
                                    </td>    
                                    <td class="label">Fecha Final</td>  
                                    <td class="input">
                                        <input id="txtFechaFin" type="text" class="css_input" size="11"  runat="server" />
                                    </td>                                     
                                </tr>
                            </table>        					
				        </div>        				
		
		                <br />
					    <div class="dv_tabla_contenedora">
  					        <table id="jqGrid_lista_A"><tr><td/></tr></table> 
						    <div id="jqGrid_pager_A"></div> 
					    </div>
		            </td>
		        </tr>
	        </table>
	    </div>    	
    </div>
    <asp:HiddenField ID="hidCodigoContrato" runat="server" />  
    
  
</form>

</body>
</html>
