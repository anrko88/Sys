<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmContratoSeguimientoListado.aspx.vb" Inherits="Comun_frmContratoSeguimientoListado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Seguimiento</title>
    
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
    <script type="text/javascript" src="../Util/js/jquery/jquery.numberformatter-1.2.3.js"></script>
    <script type="text/javascript" src="../Util/js/js_util.Grilla.js"></script>
    
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmContratoSeguimientoListado.aspx.js"></script>
	
</head>
<body>
    <form id="frmContratoSeguimientoListado" runat="server">
        
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" cellpadding="0" cellspacing="0" style="width:100%;border: 0;">
    	    <tr>
			    <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_seguimiento.gif" />
                </td>
			    <td class="titulos">
				    <div class="css_lbl_subTitulo" id="dv_subTitulo">Cotización y Negociación</div>
				    <div class="css_lbl_titulo">Seguimiento</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
				    <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:parent.fn_util_CierraModal();">
					        <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="border: 0;" /><br />
					        Cerrar
				        </a>
			        </div>
			    </td>
		    </tr>
	    </table>
	    
	    <table id="tb_tabla_comun"  cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;border: 0;" >
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
		    <tr>
			    <td class="cuerpo">
			        			    
			        <table id="jqGrid_lista_A"><tr><td/></tr></table> 
				    <div id="jqGrid_pager_A"></div>
				    
			    </td>
            </tr>
	    </table>
      </div>
      
       <input id="hddSubTitulo" name="hddCodigoContrato" type="hidden" runat="server" />
       <input id="hddCodigoContrato" name="hddCodigoContrato" type="hidden" runat="server" />
       <input id="hddCodigoSeguimiento" name="hddCodigoSeguimiento" type="hidden" runat="server" />
       <input id="hddFlagVerificaAdjunto" name="hddFlagVerificaAdjunto" type="hidden" runat="server" />
    </form>
</body>
</html>
