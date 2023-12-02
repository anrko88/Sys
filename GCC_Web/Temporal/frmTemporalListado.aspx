<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTemporalListado.aspx.vb" Inherits="Temporal_frmTemporalListado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
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
	<script type="text/javascript" src="../Util/js/jquery/json2.js" ></script>
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>
	
    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
    <script src="../Util/js/jquery/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>
    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>
    
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
    
	<!-- Local -->	
    <script src="frmTemporalListado.aspx.js" type="text/javascript"></script>
</head>
<body>
    
<form id="frmTemporalListado" runat="server">


    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
   <div id="dv_cuerpo">
    	
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="icono">
				    <img alt="" src="../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono" />		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo">Módulo Temporal</div>
				    <div class="css_lbl_titulo">Temporal :: Listado</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
    			
    				<div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_Search(true);">
						    <img alt="" src="../Util/images/ico_acc_buscar.gif" border="0"/><br />
						    Buscar
					    </a>
				    </div>	
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_LimpiarListaTemporal();">
						    <img alt="" src="../Util/images/ico_acc_limpiar.gif" border="0"/><br />
						    Limpiar
					    </a>
				    </div>	
    				
				    <div id="dv_img_boton" class="dv_img_boton_separador">
				        :
				    </div>
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_confirmaEliminar()">
						    <img alt="" src="../Util/images/ico_acc_eliminar.gif" border="0"/><br />
						    Eliminar
					    </a>
				    </div>
    					
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_abreDetalle()">
						    <img alt="" src="../Util/images/ico_acc_editar.gif" border="0"/><br />
						    Editar
					    </a>
				    </div>
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_util_redirect('frmTemporalRegistro.aspx');">
						    <img alt="" src="../Util/images/ico_acc_agregar.gif" border="0"/><br />
						    Agregar
					    </a>
				    </div>
				    
				    <div id="Div1" class="dv_img_boton">
					    <a href="javascript:VentanaAsociarBien();">
						    <img alt="" src="../Util/images/ico_acc_detalle.gif" border="0"/><br />
						    Reporte
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
							    <td class="titulo css_lbl_tituloContenido">Datos de Búsqueda</td>
							    <td class="botones">
								    <img src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" alt="" />
							    </td>
						    </tr>
					    </table>
    					
						<div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">Código</td>
                                    <td class="input"><input id="txtCodigo" type="text" class="css_input" size="10" runat="server" /></td>
                                    <td class="label">Fecha</td>
                                    <td class="input"><input id="txtFecha" type="text" class="css_input" size="15" runat="server" /></td>                                    
                                    <td class="label">Número</td>
                                    <td class="input"><input id="txtNumero" type="text" class="css_input" size="8" runat="server" /></td> 
                                </tr>
                                <tr>
                                    <td class="label">Texto</td>
                                    <td class="input"><input id="txtTexto" type="text" class="css_input" size="30" runat="server"/></td>
                                    <td class="label">Flag</td>
                                    <td class="input" colspan="3"><input id="chkFlag" type="checkbox" class="css_input" runat="server"/></td>                                                                        
                                </tr>                                   								                                
                                <tr>
                                    <td class="label" >Decimales</td>
                                    <td class="input"><input id="txtDecimales" type="text" class="css_input" size="12" runat="server"/></td>
                                    <td class="label">Comentario</td>
                                    <td class="input" colspan="3"><input id="txtComentario" type="text" class="css_input" size="50" runat="server"/></td>                                                                        
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

    <!-- Fin Cuerpo -->	
    </div>

    <input id="hddCodigo" type="hidden" runat="server" />
    
</form>

</body>
</html>
