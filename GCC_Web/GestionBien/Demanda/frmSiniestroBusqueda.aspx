<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiniestroBusqueda.aspx.vb" Inherits="GestionBien_Demanda_frmSiniestroBusqueda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documentos y Comentarios</title>
    
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    
    <!-- Estilos --> 
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css" media="all" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_fuente.css" />
    
    <!-- JavaScript -->
    <script type='text/javascript' src="../../Util/js/jquery/jquery-1.6.2.min.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>
    
    <script type="text/javascript" src="../../Util/js/js_global.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.modal.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.date.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"> </script>
    <script type="text/javascript" src="../../Util/js/jquery/jquery.dateFormat-1.0.js"></script>
    <script type="text/javascript" src="../../Util/js/jquery/jshashtable.js"></script>    
    <script type="text/javascript" src="../../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

	<!-- Local -->	
	<script type='text/javascript' src="frmSiniestroBusqueda.aspx.js"></script>
	
</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
		
		<input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
		<input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />    
		<input type="hidden" name="hddCodSiniestro" id="hddCodSiniestro" value="" runat="server" />    
		
    
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
            <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    			<tr>
					<td class="icono">
						<asp:Image ID="Image1" runat="server" 
							ImageUrl="~/Util/images/ico_mdl_demanda.gif" />
					</td>
					<td class="titulos" style="width:250px;">
						<div class="css_lbl_subTitulo">Gestión del Bien</div>
						<div class="css_lbl_titulo">Demanda :: Búsqueda de Siniestro</div>
					</td>
					<td class="espacio">&nbsp;</td>
					<td class="botones">							
    						<div id="dv_img_boton" class="dv_img_boton">
								<a href="javascript:parent.fn_util_CierraModal2();">
									<img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0"/><br />
									Cerrar
								</a>
							</div>
			    			<div id="dv_img_boton" class="dv_img_boton">
								<a href="javascript:fn_seleccionar();">
									<img alt="" src="../../Util/images/ok.gif" border="0"/><br />
									Seleccionar
								</a>
							</div>
							<div id="dv_img_boton" class="dv_img_boton">
								<a href="javascript:fn_buscaSiniestro();">
									<img alt="" src="../../Util/images/ico_acc_buscar.gif" border="0" /><br />
									Buscar </a>
							</div>
					</td>
				</tr>
			</table>
		    
			<div id="dv_contenedor" class="css_scrollPane">
		    
			<table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >
				<tr>
					<td class="lineas"></td>			
				</tr>
				<tr>
					<td class="cuerpo" style="padding-top: 0px;">
						
						<br/>
						<table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="text-align:right; width:100px"> <strong>Nro Siniestro</strong> &nbsp;</td>
                                <td>
									<input id="txtNroSiniestro" type="text" class="css_input" runat="server" size="10" />	
                                </td>
                                <td>&nbsp;</td>
                                <td class="botones">
                                    <img src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                              
                        <!-- Inicio Grilla Listado -->
                        <div class="dv_tabla_contenedoraSola">
                            <table id="jqGrid_lista_A">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_A">
                            </div>
                        </div>
                        <!-- Fin Grilla -->
						
																	
					</td>
				</tr>
			</table>
		    
		  </div>
      
      </div>

		      
      
      
    </form>
</body>
</html>
