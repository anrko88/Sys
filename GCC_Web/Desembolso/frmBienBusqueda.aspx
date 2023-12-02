<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBienBusqueda.aspx.vb" Inherits="Desembolso_frmBienBusqueda" %>

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
    <script src="frmBienBusqueda.aspx.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="frmBienes" runat="server">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
	        <tr>
		        <td class="icono"><img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" /></td>
		        <td class="css_lbl_tituloContenido">
			        <div id="css_lbl_subTitulo">Desembolso</div>
                    <div id="css_lbl_titulo">Listado de Bienes</div>
		        </td>
		        <td class="espacio">&nbsp;</td>
		        <td class="botones" style="width:300px;">
				    <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:parent.fn_util_CierraModal();">
					        <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
					        Cerrar
				        </a>
			        </div>			        
		            <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:fn_ListaBienesContrato();">
					        <img alt="" src="../Util/images/ico_acc_buscar.gif" border="0" title="Buscar" /><br />
					        Buscar
				        </a>
			        </div>				    
			        <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:fn_Limpiar()">
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
		    <tr>
	            <td class="cuerpo">
	                           
            	                
                    <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="titulo css_lbl_tituloContenido">Datos del Bien</td>
                        </tr>
                    </table> 
                    
                    
                    <asp:HiddenField ID="hddCodSolicitudCredito" Value="" runat="server" />
                    <asp:HiddenField ID="hidTipoDocumento" runat="server" />
                    <asp:HiddenField ID="hidNumeroDocumento" runat="server" />
                    <asp:HiddenField ID="hidFecEmision" runat="server" />
                    <asp:HiddenField ID="hidCodProveedor" runat="server" />
                    <asp:HiddenField ID="hddClasificacionBien" Value="" runat="server" />
                    <asp:HiddenField ID="hddEstadoDocumento" Value="" runat="server" />
                    <asp:HiddenField ID="hddTipoSubcontrato" Value="" runat="server" />
                    <input type="button" name="btnCargaBienes" id="btnCargaBienes" onclick="javascript:fn_ListaBienesContrato();" style="display:none;" /> 
                    
                    
                                        
                    
                    <div id="dv_datos" class="dv_tabla_contenedora">
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="label">Clasificación del Bien</td>
                                <td class="input"  style="width:250px;">
                                    <input id="txtClasificacionBien" type="text" class="css_input_inactivo" readonly="readonly" style="width:250px;"   /></td>
                                <td class="label" style="width:150px;">Tipo de Bien</td>
                                <td class="input">
                                    <select name="cmbTipoBien" id="cmbTipoBien">
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </div>
                
                    <br />
                    <table id="jqGrid_lista_E"><tr><td></td></tr></table> 
                    <div id="jqGrid_pager_E"></div>  
                   	            
	            </td>
	        </tr>    
	    </table>
                
                     
        

        
        
    </form>
</body>
</html>
