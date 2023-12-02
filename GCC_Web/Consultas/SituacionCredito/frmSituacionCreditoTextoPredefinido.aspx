<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSituacionCreditoTextoPredefinido.aspx.vb" Inherits="Consultas_frmSituacionCreditoTextoPredefinido" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Texto Predefinido</title>
    
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
	<script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"> </script>		
	<script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"> </script>
	
	<script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>	
	<script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>
	<script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>		
	<script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>		
	<script type="text/javascript" src="../../Util/js/jquery/json2.js" ></script>
	
	<script type="text/javascript" src="../../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../../Util/js/js_util.date.js"> </script>
	<script type='text/javascript' src="../../Util/js/js_util.ajax.js"> </script>
	
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
	<script type='text/javascript' src="frmTextoPredefinido.aspx.js"> </script>
	
</head>
<body>
    <form id="frmTextoPredefinido" runat="server">
        
     <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
	    <table id="tb_cuerpoCabecera" cellpadding="0" cellspacing="0" style="width:100%; border: 0;">
	        <tr>
                <td class="icono">
	                <img src="../../Util/images/ico_mdl_multa.gif" class="jd_menu_icono" alt="" />
                </td>
                <td class="titulos">
	                <div class="css_lbl_subTitulo"> </div>
	                <div class="css_lbl_titulo">Texto Predefinido</div>
                </td>
                <td class="espacio">&nbsp;</td>
                <td class="botones">
    			
		            <div id="dv_img_boton_cerrar" class="dv_img_boton">
		                <a href="javascript:parent.fn_util_CierraModal();">
			                <img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="border: 0px;" /><br />
			                Cerrar
		                </a>
	                </div>
				
                </td>
            </tr>
        </table>	
        
        
	    <table id="tb_tabla_comun" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px; border: 0px;" >
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
	    </table>		    
        <br />
        
        <div id="dvtipoObservaciones">    
            <table id="tb_formulario" style="border: 0px; padding: 0px;" cellspacing="0">
                <tr>                        
                    <td class="input" style="text-align:center;border: 1px;">
                        <div style="text-align:left;border: 1px;">
                            <textarea id="txaTextoPredefinido" name="txaTextoPredefinido" rows="16" cols="90" style="width: 98%; 
                                font-family: Courier New; font-size: 11px;" runat="server" readonly="readonly" disabled="disabled" ></textarea>
                            <br />
                        </div>
                    </td>
                </tr>
            </table>
        </div>   
            
    </div>
    
    <input id="hddCodigoContratoDocumento" type="hidden" runat="server" />
    <input id="hddNuevo" type="hidden" runat="server" />
    <input id="hddCodigoContrato" type="hidden" runat="server" />
    <input id="hddEdita" type="hidden" runat="server" />
    
    </form>
</body>
</html>
