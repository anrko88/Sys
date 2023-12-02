<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRepresentanteRegistroIbk.aspx.vb" Inherits="Comun_frmRepresentanteRegistroIbk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
	<script type='text/javascript' src="frmRepresentanteRegistroIbk.aspx.js"></script>
</head>
<body>
    
    <form id="frmSubirArchivo" runat="server">
     <!-- BOTON OCULTO PARA REGRESACAR LA PAGINA ANTERIOR -->	   
    <asp:Button ID="cmdguardar" name="cmdguardar" runat="server" Text="Button" style="display:none;"   />
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
	   <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
            <tr>
                <td class="icono"><img src="../Util/images/ico_mdl_multa.gif" class="jd_menu_icono"/></td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo"> </div>
                    <div class="css_lbl_titulo">Representantes</div>
                </td>
                <td class="espacio">&nbsp;</td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                    <a href="javascript:parent.fn_util_CierraModal();">
                    <img src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
                    Cerrar
                    </a>
                    </div>

                    <div id="Div3" class="dv_img_boton_separador">
                    :
                    </div>

                    <div id="dv_img_boton" class="dv_img_boton">
                    <a href="javascript:fn_GuardarRepresentanteNuevo();">
                    <img src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
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
	        
        <br />
      
        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%;">                                 
        <tr>
                <td class="label">Contrato Firmarse en </td>			                                        
                <td class="input" style="width: 189px">
                <select id="cmbcontratofirma" name="cmbcontratofirma" runat="server" onchange="javascript:fn_ubicacionfirmar(this.value);" >
                </select>
                </td>

                <td class="label" id="td_lblDepartamento" style="width: 92px">Departamento</td>
                <td class="input" id="td_InpDepartamento">
                <select id="cmbDepartamento" name="cmbDepartamento" onchange="javascript:fn_cargaComboProvincia(this.value);" runat="server">
                <option value="0">[- Seleccionar -]</option>                                                    
                </select>
                </td>
        </tr>
        <tr id="tr_ProvDist" >
                <td class="label">Provincia</td>
                <td class="input" style="width: 189px">
                <select id="cmbProvincia" name="cmbProvincia" onchange="javascript:fn_cargaComboDistrito(cmbDepartamento.value,this.value);" runat="server" >
                <option value="0">[- Seleccionar -]</option>                                                    
                </select>
                </td>
                <td class="label" style="width: 92px">Distrito</td>
                <td class="input">
                <select id="cmbDistrito" name="cmbDistrito" runat="server">
                <option value="0">[- Seleccionar -]</option>                                      
                </select>
                </td>
        </tr>
        <tr id="trDatoRepresentante" >
                <td class="label">DNI</td>
                <td class="input" style="width: 189px"><input id="txtNroDocumento" name="txtNroDocumento" type="text" class="css_input" size="13" value=""  runat="server" /></td>
                <td class="label" style="width: 92px">Nombres</td>
                <td class="input"><input id="txtNombreRepresentante" name="txtNombreRepresentante" type="text" class="css_input" size="30" value=""  runat="server" /></td>
        </tr>
        <tr>
                <td class="label">Partida Registral</td>
                <td class="input" style="width: 189px"><input id="txtPartidaRegistral" name="txtPartidaRegistral" type="text" class="css_input" size="10"  runat="server"/></td>
                <td class="label" style="width: 92px">Oficina Registral</td>
                <td class="input"><input id="txtOficinaRegistral" name="txtOficinaRegistral" type="text" class="css_input" size="20"  runat="server" /></td>

        </tr>
        </table>
   
	            
    </div>
   
    <asp:HiddenField ID="hddCodContrato" runat="server" />
    <asp:HiddenField ID="hFirmaen" runat="server" />
    <asp:HiddenField ID="hubigeo" runat="server" />
    
    <asp:HiddenField ID="hdepartamento" runat="server" />
    <asp:HiddenField ID="hprovincia" runat="server" />
    <asp:HiddenField ID="hdistrito" runat="server" />
  
  <asp:HiddenField ID="hddCadenaUbigeo" runat="server" />
  

    </form>
</body>
</html>
