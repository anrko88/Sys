<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSegurosListado.aspx.vb" 
Inherits="GestionBien_frmSegurosListado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    
     <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
    <!-- Estilos --> 
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css"
        media="all" />
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
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmSegurosListado.aspx.js"> </script>
    
    
</head>
<body>
    <form id="frmSegurosListado" runat="server">
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
				    <div class="css_lbl_subTitulo">Reporte</div>
				    <div class="css_lbl_titulo">Reporte :: Seguro</div>
			    </td>
			    <td class="espacio">
			        &nbsp;    								
			    </td>
			    <td class="botones">
    				<div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscarSeguros(true);">
                            <img alt="" src="../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
				    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_limpiar();">
                            <img src="../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
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
								    <td class="label">N° Póliza</td>
							        <td class="input"><input id="txtNroPoliza" type="text" class="css_input" size="20"/></td>
							        <td class="label">Cia de Seguros</td>
							        <td class="input"><input id="txtCiaSeguros" type="text" class="css_input" runat="server" size="55" /></td>
							    </tr>
							    <tr>
								    <td class="label">Tipo de Valor</td>
							        <td class="input">
							            <select id="cmbTipoValor" runat="server">
									        <option>- Seleccionar -</option>
								        </select>
							        </td>			
							         <td class="label">Fecha Inicio Póliza</td>
							        <td class="input"><input id="txtFechaIni" type="text" class="css_input" size="11"/></td>
							        <td class="label">Fecha Fin Póliza</td>
							        <td class="input"><input id="txtFechaFin" type="text" class="css_input" size="11"/></td>
							    </tr>
							    <tr>
							        <td class="label">Razón Social</td>
							        <td class="input"><input id="txtTipoBien" type="text" class="css_input" size="50"/></td>
							        <td class="label">Tipo de Seguro</td>
							        <td class="input" colspan="3">
							            <select id="cmbTipoSeguro" runat="server">
									        <option>- Seleccionar -</option>
								        </select>
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

    <!-- Fin Cuerpo -->		
    </div>
    <div>
       
        <input id="hdNroContrato" type="hidden" runat="server" value="" />
        <input id="hdNroPoliza" type="hidden" runat="server" value="" />
        <input id="hdTipoSeguro" type="hidden" runat="server" value="" />
        <input id="hdCiaSeguro" type="hidden" runat="server" value="" />
        <input id="hdNroPrenda" type="hidden" runat="server" value="" />
        <input id="hdCliente" type="hidden" runat="server" value="" />
        <input id="hdFini" type="hidden" runat="server" value="" />
        <input id="hdFfin" type="hidden" runat="server" value="" />
    </div>
    </form>
</body>
</html>
