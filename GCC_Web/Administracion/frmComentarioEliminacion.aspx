<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmComentarioEliminacion.aspx.vb" Inherits="Administracion_frmComentarioEliminacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Motivo de Rechazo</title>
    
    <!-- Icono URL -->
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
	
    <!-- JQGrid -->		
    
    
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmComentarioEliminacion.aspx.js"></script>
	
</head>
<body>
    <form id="frmContratoSeguimiento" runat="server">
    
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
            <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    	    <tr>
			    <td class="icono">
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Util/images/ico_mdl_cotizacion.gif" />
                </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo">Mant. Bien</div>
				    <div class="css_lbl_titulo">Deshabilitar Bien</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
    				    <div id="dv_img_boton" class="dv_img_boton">
					        <a href="javascript:parent.fn_util_CierraModal();">
						        <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						        Cerrar
					        </a>
				        </div>
    				    <div id="div_Separador" class="dv_img_boton_separador">
		    		        :
	    			    </div>
			
			    	    <div  class="dv_img_boton" id="div_guardar" runat="server">
					        <a href="javascript:fn_DeshabilitarBienes();">
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
	    </table>
	    
	    <input type="hidden" name="hddCodigoContrato" id="hddCodigoContrato" value="" runat="server" />
        <input type="hidden" name="hddCodigoSecFinanciamiento" id="hddCodigoSecFinanciamiento" value="" runat="server" />
        <input type="hidden" name="" id="hidOpcion" value="" runat="server" />
                <input type="hidden" name="" id="hidCodClasificacion" value="" runat="server" />

    
	    <asp:Button ID="btnGrabar" runat="server" Style="display:none;" Text="Graba" />
                
        <table id="tb_formulario" border="0" cellpadding="0">
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td class="label">
                Fecha
            </td>
            <td class="input">
                <input id="txtFecha" name="txtFecha" type="text" class="css_input" size="11" runat="server"/>
                
                <asp:HiddenField ID="HidFecha" runat="server" />
            </td>
          </tr>
          <tr>
            <td class="label" valign="top" style="vertical-align: top">Comentario</td>
            <td class="input">
                <textarea name="txtComentario" id="txtComentario" cols="75" rows="6" runat="server"></textarea>
            </td>
          </tr>
      
                    
          </table>
      </div>
    </form>
</body>
</html>
