<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmLogin.aspx.vb" Inherits="frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    	
	<!-- Icono URL -->
	<link rel="SHORTCUT ICON" href="Util/images/PV16x16.ico" /> 

	<!-- JavaScript -->
	<script type='text/javascript' src="Util/js/jquery/jquery-1.6.2.min.js"> </script>
	<script type='text/javascript' src="Util/js/jquery/jquery-ui-1.8.13.custom.min.js"> </script>
	<script type='text/javascript' src="Util/js/jquery/jquery.blockUI.js"> </script>		
	<script type='text/javascript' src="Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="Util/js/js_util.funcion.js"> </script>

	<!-- Estilos -->
	<link type="text/css" rel="stylesheet" href="Util/css/jquery/jquery-ui-1.8.15.custom.css" />
	<link type="text/css" rel="stylesheet" href="Util/css/css_global.css" />
	<link type="text/css" rel="stylesheet" href="Util/css/css_formulario.css" />
	<link type="text/css" rel="stylesheet" href="Util/css/css_fuente.css" />
			
	<!-- Local -->	
	<script type='text/javascript' src="frmLogin.aspx.js"> </script>
	
</head>
<body>

    <form id="frmLogin" runat="server">

	    <table id="tb_tabla_logueo" border="0" cellpadding="0" cellspacing="0" align="center">
		    <tr>
			    <td class="cabecera" colspan="2"></td>			
		    </tr>
		    <tr>
			    <td class="icono"></td>
			    <td class="cuerpo">			
    	
				    <div id="dv_datos" class="dv_tabla_contenedora" style="width:250px; padding:5px; margin-right:5px;">    				
					    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:255px;">
						    <tr>
							    <td class="label" style="width:100px;">
								    Usuario
							    </td>
							    <td class="input">								    
								    <asp:TextBox runat="server" ID="txtUsuario" CssClass="inputform" style="width: 80px" MaxLength="10"></asp:TextBox> 
							    </td>
						    </tr>
						    <tr>
							    <td class="label" style="width:100px;">
								    Clave
							    </td>
							    <td class="input">								    
								    <asp:TextBox runat="server" ID="txtClave" CssClass="inputform" 
                                        style="width: 80px" MaxLength="10" TextMode="Password"></asp:TextBox> 
							    </td>
						    </tr>
						    <tr>
							    <td class="label" style="width:100px;">
								    Perfil
							    </td>
							    <td class="input">								    
								    <asp:DropDownList ID="ddlPerfil" runat="server">                                   
                                    </asp:DropDownList>
							    </td>
						    </tr>						    
						    <tr>
							    <td colspan="2" align="right" style="height:25px;">
								    <input type="button" id="btnIngresar" value="Ingresar" class="css_btn_general" />
								    <asp:Button ID="btnIngresarHide" runat="server" Style="display:none;" Text="IngresarHidden" /> 
							    </td>
						    </tr>
					    </table>    				
				    </div>    				
			    </td>			
		    </tr>
		    <tr>
			    <td colspan="2">					    
                    <asp:Label ID="Label1" runat="server" style="font-weight:bold;color:Red;" CssClass="label"></asp:Label>
			    </td>
		    </tr>
	    </table>
	    _
    </form>    
</body>
</html>
