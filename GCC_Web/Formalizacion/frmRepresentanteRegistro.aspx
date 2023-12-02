<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRepresentanteRegistro.aspx.vb" Inherits="Formalizacion_frmRepresentanteRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro Representante</title>
    
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
	
    <script type='text/javascript' src="../Util/js/jquery/jquery.ui.global.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.validText.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.validNumber.js"></script>
	<script type='text/javascript' src="../Util/js/jquery/jquery.maxlength.js"></script>
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.date.js"></script>
    <script type='text/javascript' src="../Util/js/js_util.ajax.js"></script>
    
    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>
    
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmRepresentanteRegistro.aspx.js"></script>
	
</head>
<body>
    <form id="frmRepresentanteRegistro" runat="server">
        
    <asp:Button ID="cmdGuardarRepresentante" name="cmdGuardarRepresentante" runat="server" Text="Guardar Representante" style="display:none;" />
            
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        
	    <table id="tb_cuerpoCabecera" cellpadding="0" cellspacing="0" style="width:100%; border: 0;">
	        <tr>
                <td class="icono">
	                <img alt="" src="../Util/images/ico_mdl_multa.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
	                <div class="css_lbl_subTitulo">Representantes Cliente</div>
	                <div class="css_lbl_titulo">Representante :: Nuevo</div>
                </td>
                <td class="espacio">&nbsp;</td>
                <td class="botones">
    			
		            <div id="dv_img_boton" class="dv_img_boton">
		                <a href="javascript:fn_CierraModal();">
			                <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="border: 0px;" /><br />
			                Cerrar
		                </a>
	                </div>
				    
	                <div id="Div3" class="dv_img_boton_separador">
	                    :
	                </div>
    				                				
	                <div id="dv_img_boton" class="dv_img_boton" style="width:75px;">
		                <a href="javascript:fn_SeleccionarRepresentantes();">
			                <img alt="" src="../Util/images/ok.gif" style="border: 0px; width: 35px;" /><br />
			                Seleccionar
		                </a>
	                </div>	
	                
                </td>
            </tr>
        </table>	
        
	    <table id="tb_tabla_comun" cellpadding="0" cellspacing="0" style="width:100%; padding: 0px;border: 0px;">
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
		    <tr>
		        <td>&nbsp;</td>
		    </tr>
		    <tr>
		        <td>
    				    
			        <table id="tb_tabla_comunCabecera" cellpadding="0" cellspacing="0" style="width:100%;border: 0;">
				        <tr>
					        <td class="titulo css_lbl_tituloContenido">Nuevo Representante</td>
				        </tr>
			        </table>

			    </td>
		    </tr>
		    <tr>
		        <td>&nbsp;</td>
		    </tr>
	    </table>

        <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
            <colgroup>                           
                <col style="width:13%;" />
                <col style="width:20.33%;" />
                <col style="width:13%;" />
                <col style="width:20.33%;" />
                <col style="width:13%;" />
                <col style="width:20.33%;" />
            </colgroup>
            			
            <tr>
                <td class="label">Departamento</td>
                <td class="input">
                    <select id="cmbDepartamento" name="cmbDepartamento" runat="server" onchange="javascript:fn_cargaComboProvincia('#cmbProvincia', '#cmbDistrito', this.value);fn_AsignarOficinaRegistral(this.value);">
                    </select>
                </td>
                <td class="label">Provincia</td>
                <td class="input">
                    <select id="cmbProvincia" name="cmbProvincia" onchange="javascript:fn_cargaComboDistrito('#cmbDistrito', cmbDepartamento.value, this.value);">
                        <option value="0">[- Seleccionar -]</option>
                    </select>
                </td>
                <td class="label">Distrito</td>
                <td class="input">
                    <select id="cmbDistrito" name="cmbDistrito">
                        <option value="0">[- Seleccionar -]</option>
                    </select>
                </td>
            </tr>	           
            <tr>
                <td class="label">Tipo Documento</td>
                <td class="input">
                    <select id="cmbTipoDocumento" name="cmbTipoDocumento" runat="server">
                    </select>
                </td>
                <td class="label">Nro. Documento</td>
                <td class="input"><input id="txtNroDocumento" name="txtNroDocumento" type="text" class="css_input" size="14" /></td>
                <td class="label">Representante</td>
                <td class="input"><input id="txtNombreRepresentante" name="txtNombreRepresentante" type="text" class="css_input" size="40" /></td>
            </tr>
            <tr>
                <td class="label">Partida Registral</td>
                <td class="input"><input id="txtPartidaRegistral" name="txtPartidaRegistral" type="text" class="css_input" size="14" /></td>
                <td class="label">Oficina Registral</td>
                <td class="input"><input id="txtOficinaRegistral" name="txtOficinaRegistral" type="text" class="css_input" size="20" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

        <br />
        <table id="tb_formulario1" cellpadding="0" cellspacing="0" style="border: 0;">
            <tr>
                <td style="text-align: left;">
                
                    <div id="dv_AccionesRepresentante" style="border:0;padding-right:10px;">
                        
                         <div id="dv_img_Agregar" class="dv_img_boton_mini" style="border:0;height:22px;float:right;">
                            <a href="javascript:fn_GuardarRepresentanteNuevo();">
                                <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width:16px; height:16px;border:0;" />&nbsp;Agregar
                            </a>
                         </div>
                    
                         <div id="dv_img_Editar" class="dv_img_boton_mini" style="border:0;height:22px;float:right;">
                            <a href="javascript:fn_EditarRepresentante();">
                                <img alt="" src="../Util/images/ico_acc_editar.gif" style="width:16px; height:16px;border:0;" />&nbsp;&nbsp;&nbsp;Editar
                            </a>
                         </div>                                     

                         <div id="dv_img_Eliminar" class="dv_img_boton_mini" style="border:0;height:22px;float:right;display: none;visibility: hidden;">
                            <a href="javascript:fn_eliminarRepresentante();">
                                <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width:16px; height:16px;border:0;" />Eliminar
                            </a>
                         </div>
                    
                    </div>
                    
                    <div id="dv_ProcesoRepresentante" style="border:0;padding-right:10px;">
               
                        <div id="dv_img_Guardar" class="dv_img_boton_mini" style="border:0;height:22px;float:right;">
                            <a href="javascript:fn_GuardarRepresentante();">
                                <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width:16px; height:16px;border:0;" />Guardar
                            </a>
                        </div>  
            
                        <div id="dv_img_Cancelar" class="dv_img_boton_mini" style="border:0;height:22px;float:right;">
                            <a href="javascript:fn_CancelarRepresentante();">
                                <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width:16px; height:16px;border:0;" />&nbsp;&nbsp;&nbsp;Cancelar
                            </a>
                        </div>
                        
                    </div>     
                                                                           
                </td>
             </tr>
        </table>
  	            
    </div>
    
    <table id="jqGrid_lista_AA"><tr><td/></tr></table> 
    <div id="jqGrid_pager_A"></div>
    
    <div>
        <asp:HiddenField ID="hdnTitulo" runat="server" />
        <asp:HiddenField ID="hdnCodigoRepresentante" runat="server" />
        <asp:HiddenField ID="hflagtipoObs" runat="server" />
        <input id="hddCodigoTipoRepresentante" type="hidden" runat="server" />
        <input id="hddCodUnico" type="hidden" runat="server" />
        <input id="hdnCodigoContrato" type="hidden" runat="server" />
        <input id="hddCambiosSinGuardar" type="hidden" />
    </div>
                    
    </form>
</body>
</html>
