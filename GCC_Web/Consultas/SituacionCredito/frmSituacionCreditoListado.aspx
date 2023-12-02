<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSituacionCreditoListado.aspx.vb" Inherits="SituacionCredito_frmSituacionCreditoListado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
	
	<!-- Icono URL -->
	<link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
	
	<!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />					
	<link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css" media="all" />
	<link type="text/css" rel="stylesheet" href="../../Util/css/css_global.css" />
	<link type="text/css" rel="stylesheet" href="../../Util/css/css_formulario.css" />
	<link type="text/css" rel="stylesheet" href="../../Util/css/css_fuente.css" />
	
	<!-- JavaScript -->
    <script type='text/javascript' src="../../Util/js/jquery/jquery-1.6.2.min.js"></script>
	<script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>
	<script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"></script>		
	<script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"></script>
	
	<script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"></script>	
	<script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"></script>
	<script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"></script>		
	<script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"></script>	
	
	<script type="text/javascript" src="../../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../../Util/js/js_util.modal.js"></script>
	<script type='text/javascript' src="../../Util/js/js_util.funcion.js"></script>	
	<script type='text/javascript' src="../../Util/js/js_util.date.js"></script>		
    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"></script>	
    
    <script src="../../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
    <script src="../../Util/js/jquery/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>
    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>
    
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmSituacionCreditoListado.aspx.js"></script>
	
</head>
<body>   
<form id="frmContratoListado" runat="server">

    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
    	<!-- Botones de Cabezera -->
	    <table id="tb_cuerpoCabecera" style="border: 0;" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="icono">
				    <img alt="" src="../../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />		
			    </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo">Consultas</div>
				    <div class="css_lbl_titulo">Situación del Crédito :: Listado</div>
			    </td>
			    <td class="espacio">
			        &nbsp;    								
			    </td>
			    <td class="botones">
    				<div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_buscarContratos(true);">
						    <img  src="../../Util/images/ico_acc_buscar.gif" style="border: 0;"/><br />
						    Buscar
					    </a>
				    </div>	
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_limpiarContrato();">
						    <img  src="../../Util/images/ico_acc_limpiar.gif" style="border: 0;"/><br />
						    Limpiar
					    </a>
				    </div>	
				    
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_abreDetalle();">
						    <img src="../../Util/images/ico_acc_ver.gif" style="border: 0;" /><br />
						    Ver
					    </a>
				    </div>	
				    <div id="dv_img_boton" class="dv_img_boton">
					    <a href="javascript:fn_Reporte();">
						    <img  src="../../Util/images/ico_acc_importar.gif" style="border: 0;" /><br />
						    Exportar
					    </a>
				    </div>
 
			    </td>
		    </tr>
	    </table>	
    	
	    <div id="dv_contenedor" class="css_scrollPane">
		    <table id="tb_tabla_comun" style="border: 0;" cellpadding="0" cellspacing="0">
			    <tr>
				    <td class="lineas"></td>			
			    </tr>
			    <tr>
				    <td class="cuerpo">
                        <asp:Button ID="btnGenerar" runat="server" Style="display: none" />
					    <table id="tb_tabla_comunCabecera" style="border: 0;" cellpadding="0" cellspacing="0">
						    <tr>
							    <td class="titulo css_lbl_tituloContenido">Datos de búsqueda</td>
							    <td class="botones">
								    <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
							    </td>
						    </tr>
					    </table>
    					
					    <div class="dv_tabla_contenedora">
						    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
						        <colgroup>                           
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                    <col style="width:16.66%;" />
                                </colgroup> 
						        
							    <tr>
							        <td class="label">Nº Contrato</td>
							        <td class="input"><input id="txtContrato" name="txtContrato" type="text" class="css_input" size="12" tabindex="1" runat="server" /></td>
		        				    <td class="label">CU Cliente</td>
								    <td class="input"><input id="txtCuCliente" name="txtCuCliente" type="text" class="css_input" tabindex="2" runat="server" /></td>								    
								    <td class="label">Razón Social o Nombre</td>
								    <td class="input"><input id="txtRazonSocial" name="txtRazonSocial"  type="text" class="css_input" size="70" tabindex="3" runat="server" /></td>
							    </tr>
							    <tr>
							        <td class="label">N° Cotización</td>
							        <td class="input"><input id="txtCotizacion" name="txtCotizacion" type="text" class="css_input" size="12" tabindex="4" runat="server" /></td>
							        <td class="label">Fecha Inicio</td>
								    <td class="input"><input id="txtFechaIni" name="txtFechaIni" type="text" class="css_input" size="11" tabindex="5" runat="server" /></td>
								    <td class="label">Fecha Final</td>
							        <td class="input"><input id="txtFechaFin" name="txtFechaFin" type="text" class="css_input" size="11" tabindex="6" runat="server" /></td>
							    </tr>
							    <tr>
								    <td class="label">Ejecutivo</td>
								    <td class="input">
								        <select id="cmbEjecutivo" name="cmbEjecutivo" runat="server" tabindex="7">
                                        </select>
                                    </td>
								    <td class="label">Estado</td>
								    <td class="input">
							            <select id="cmbEstado" name="cmbEstado" runat="server" tabindex="8">
                                        </select>
                                    </td>								    
								    <td class="label">Zonal</td>
							        <td class="input">
                				       <input id="txtZonal" type="text" runat="server" />
								    </td>								    								    								    
							    </tr>							    
							    <tr>
							        <td class="label">Clasificación de Bien</td>
							        <td class="input">
								        <select id="cmbClasificacion" name="cmbClasificacion" runat="server" tabindex="10">
								        </select>
							        </td>							        
							        <td class="label">Clasificación del Contrato</td>
							        <td class="input">
									    <select id="cmbClasificacionContrato" name="cmbClasificacionContrato" runat="server" tabindex="11">
								        </select>
								    </td>
					                <td class="label">Banca</td>
							        <td class="input">
							            <select id="cmbBanca" name="cmbBanca" runat="server" tabindex="12">
                                        </select>
						            </td>
							    </tr>
							    <tr>
							     <td class="label">Tipo de Persona</td>
							     <td class="input">
							        <select id="cmbTipoPersona" name="cmbTipoPersona" runat="server" tabindex="13">
							        </select>
                                  </td>
                                  
                                  <td class="label">Notaria</td>
                                  <td class="input">
                                    <select id="cmbNotaria" name="cmbNotaria" runat="server" tabindex="14">
                                    </select>
                                  </td>
                                  
                                  <td class="label">Nº Kardex</td>  
                                  <td class="input"><input id="txtKardex" name="txtKardex" type="text" class="css_input" size="11" tabindex="15" runat="server" /></td>	
                                  
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
    
    <input type="button" name="btnListar" id="btnListar" onclick="javascript:fn_buscarListarContratos();" style="display:none;" />
    <input id="hddCodigoContrato" type="hidden" runat="server" />
    
    <input id="hddId" type="hidden" />
    <input id="hddNombreSubprestatario" type="hidden" />
    <input id="hddCorreocontacto" type="hidden" />
    <input id="hddMensajeCorreo" type="hidden" runat="server" />
        
</form>

</body>
</html>