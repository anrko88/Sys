<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSubirArchivo.aspx.vb" Inherits="Formalizacion_frmSubirArchivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subir archivo</title>
    
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
	
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmSubirArchivo.aspx.js"></script>
	
</head>
<body>
    <form id="frmSubirArchivo" runat="server">
        
     <!-- BOTON OCULTO PARA REGRESAR A LA PÁGINA ANTERIOR -->	   
    <asp:Button ID="cmdguardar" runat="server" Text="Button" style="display:none;"   />
    
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
	   <table id="tb_cuerpoCabecera" cellpadding="0" cellspacing="0" style="width:100%; border: 0;">
            <tr>
                <td class="icono"><img alt="" src="../Util/images/ico_mdl_multa.gif" class="jd_menu_icono" /></td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo"> </div>
                    <div class="css_lbl_titulo">Subir Archivo</div>
                </td>
                <td class="espacio">&nbsp;</td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                        <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="border: 0;" /><br />
                        Cerrar
                        </a>
                    </div>

                    <div id="Div3" class="dv_img_boton_separador">
                    :
                    </div>

                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Guardar();">
                        <img alt="" src="../Util/images/ico_acc_grabar.gif" style="border: 0;" /><br />
                        Guardar
                        </a>
                    </div>	
                </td>
            </tr>
        </table>	
	    <table id="tb_tabla_comun" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;border: 0;">
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
	    </table>	
	        
        <br />
        <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
	        <tr>
                <td class="label">Adjuntar Archivo</td>
                <td class="input">
                     <asp:FileUpload ID="txtArchivoDocumentos" runat="server" />
                </td>
            </tr>
	        <tr>
                <td>&nbsp;</td>
                <td class="input">
                    <div style="visibility: hidden;display: none;">
                         <asp:CheckBox ID="chkValidarArchivoExistente" runat="server" CssClass="input" Visible="False"
                             Text="Reemplazar archivo existente" />
                    </div>
                </td>
            </tr>
        </table>
	            
    </div>
 
    <div>
        <asp:HiddenField ID="hdnTitulo" runat="server" />
        <asp:HiddenField ID="hddflaCondAdicional" runat="server" />
        
        <asp:HiddenField ID="hddCodConDoc" runat="server" />
        <asp:HiddenField ID="hddCodContrato" runat="server" />
        <asp:HiddenField ID="hddFormulario" runat="server" />

        <asp:HiddenField ID="hddFile" runat="server" />
        <asp:HiddenField ID="hddDirectorio" runat="server" />
        <asp:HiddenField ID="hddOp" runat="server" />
        <input id="hddControl" type="hidden" runat="server" />
        <input id="hddNombreArchivo" type="hidden" runat="server" />	            
    </div>
    
    </form>
</body>
</html>