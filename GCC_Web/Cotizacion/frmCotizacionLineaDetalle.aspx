<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCotizacionLineaDetalle.aspx.vb" Inherits="Cotizacion_frmCotizacionLineaDetalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Motivo de Rechazo</title>
    
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
    
    
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmCotizacionLineaDetalle.aspx.js"></script>
	
</head>
<body>
    <form id="frmCotizacionLineaDetalle" runat="server">
    
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
				    <div class="css_lbl_subTitulo">Cotización y Negociación</div>
				    <div class="css_lbl_titulo">Detalle Linea</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
    				    <div id="dv_img_boton" class="dv_img_boton">
					        <a href="javascript:parent.fn_util_CierraModal();">
						        <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						        Cerrar
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
	    
        <table id="tb_formulario" border="0" cellpadding="0">
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td class="label">Monto Disponible</td>
            <td class="input"><input id="txtMontoDisponible" name="txtMontoDisponible" type="text" class="css_input_inactivo" size="14" runat="server" /></td>
            <td class="label">Monto Aprobado</td>
            <td class="input"><input id="txtMontoAprobado" name="txtMontoAprobado" type="text" class="css_input_inactivo" size="14" runat="server"/></td>
          </tr>
          <tr>
            <td class="label">Monto Utilizado</td>
            <td class="input"><input id="txtMontoUtilizado" name="txtMontoUtilizado" type="text" class="css_input_inactivo" size="14" runat="server"/></td>
            <td class="label">Estado</td>
            <td class="input"><input id="txtEstado" name="txtEstado" type="text" class="css_input_inactivo" size="14" runat="server"/></td>
          </tr>
          <tr>
            <!--
            <td class="label">Fecha Aprobación</td>
            <td class="input"><input id="txtFechaAprobacion" type="text" class="css_input" value="" size="11" runat="server"/></td>
            -->
            <td class="label">Fecha Vencimiento</td>
            <td class="input"><input id="txtFechaVence" type="text" class="css_input_inactivo" size="11" runat="server"/></td>
          </tr>
          </table>
      </div>
    </form>
</body>
</html>
