<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEnviarCartaCli.aspx.vb" Inherits="Verificacion_frmEnviarCartaCli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enviar Correo</title>
    <!-- Icono URL -->
	<link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
	
	<!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />					
	<link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css" media="all" />
	<link type="text/css" rel="stylesheet" href="../Util/css/css_global.css" />
	<link type="text/css" rel="stylesheet" href="../Util/css/css_formulario.css" />
	<link type="text/css" rel="stylesheet" href="../Util/css/css_fuente.css" />
	
	<!-- JavaScript -->
	<script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.js"> </script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"> </script>		
	<script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"> </script>
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>	

    <script src="frmEnviarCartaCli.aspx.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmEnviarCartaCli" runat="server">
    
    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >    
        <tr>
            <th>
            
            <br />
            <table border="0" cellpadding="0" width="80%">        
            <tr>
                <td>
                    ¿Está Seguro de Enviar el Correo al Cliente?
                </td>
            </tr>
            </table>
            <br />        
                 
            <center>
                <asp:Button ID="btnAceptar" runat="server" Text="SI" CssClass="css_btn_general" Width="50px"/>        
                <input type="button" id="btnCancelar" value="NO" class="css_btn_general" onclick="javascript:parent.fn_util_CierraModal();" style="width:50px"/>                
            </center>
            
            </th>
        </tr>
    </table>       
    </form>
</body>
</html>
