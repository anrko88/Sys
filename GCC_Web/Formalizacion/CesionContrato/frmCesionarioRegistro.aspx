<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCesionarioRegistro.aspx.vb" Inherits="Formalizacion_CesionContrato_frmCesionarioRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documentos y Comentarios</title>
    
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
    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>
    <script type="text/javascript" src="../../Util/js/jquery/json2.js"></script>
    <script type="text/javascript" src="../../Util/js/js_global.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.modal.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.date.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"> </script>
    <script src="../../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
        
    <script type="text/javascript" src="../../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmCesionarioRegistro.aspx.js"></script>
	
</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
    
		<input type="hidden" name="hddTipoTx" id="hddTipoTx" value="" runat="server" />
		<input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />		
		<input type="hidden" name="hddCodCesionario" id="hddCodCesionario" value="" runat="server" />		
		<input type="button" name="btnBuscar" id="btnBuscar" onclick="javascript:fn_listaCesionarios();" style="display: none;" />
		
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
            <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    	    <tr>
			    <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_cesion.gif" />
                </td>
			    <td class="titulos" style="width:450px;">
				    <div class="css_lbl_subTitulo">Formalización</div>
				    <div class="css_lbl_titulo">Cesión de Contrato :: <span id="sp_accion">Registro Cesionario</span> </div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
			    
    				    <div id="dv_img_boton" class="dv_img_boton">
					        <a href="javascript:parent.fn_util_CierraModal();">
						        <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						        Cerrar
					        </a>
				        </div>	
				        
				        <div id="dv_img_boton" class="dv_img_boton">
							<a href="javascript:fn_limpiar();">
								<img src="../../Util/images/ico_acc_limpiar.gif" border="0" /><br />
								Limpiar </a>
						</div>
                    		
				        
			    </td>
		    </tr>
	    </table>
	    
	    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
		    <tr>
				<td class="cuerpo">
										
					<div class="dv_tabla_contenedora">
                        
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                        
							<tr>
                                <td class="label">
                                    Tipo Documento
                                </td>
                                <td class="input">
                                    <select id="cmdTipoDoc" name="cmdTipoDoc" runat="server">
                                    </select>
                                </td>
                                <td class="label" style="width:100px;">
                                    Nº Documento
                                </td>
                                <td class="input">
                                    <input id="txtNroDocumento" type="text" class="css_input" size="13" />
                                    <img id="imgBsqClienteRM" alt="Validar con RM" src="../../Util/images/ico_buscar.jpg" style="cursor:pointer; vertical-align:middle;" runat="server" />
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td class="label">
                                    Cod. Unico
                                </td>
                                <td class="input">
									<input type="text" name="txtCodUnico" id="txtCodUnico" value="" runat="server" disabled="disabled" size="12" />
                                </td>                                                                                                          
                                <td class="label">
                                    Denominación o razón social
                                </td>
                                <td class="input" colspan="3">
									<input id="txtRazSocial" type="text" class="txtRazSocial" runat="server" size="71" />
                                </td>                                                                                             
                            </tr>
                        
                            <tr>
                                <td class="label" style="width:120px;">
                                    Dirección
                                </td>
                                <td class="input" colspan="5">
									<input id="txtDireccion" type="text" class="css_input" runat="server" size="71" />
                                </td>                                                                                             
                            </tr>
                            
                            <tr>
								<td class="label">Departamento</td>
                                <td class="input" style="width:170px;">
                                    <select id="cmbDepartamento" name="cmbDepartamento" onchange="javascript:fn_cargaComboProvincia(this.value,'0');" runat="server">
                                       <option value="0">[-Seleccionar-]</option>                                                  
                                    </select>
                                </td>      
                                
                                <td class="label" style="width:100px;">
                                    Provincia 
                                </td>
                                <td class="input" style="width:180px;">
                                     <select id="cmbProvincia" name="cmbProvincia" onchange="javascript:fn_cargaComboDistrito(cmbDepartamento.value,this.value,'0');" runat="server">
										<option value="0">[-Seleccionar-]</option>														
                                     </select>
                                </td>
                                <td class="label" style="width:80px;">Distrito</td>
                                <td class="input">
                                     <select id="cmbDistrito" name="cmbDistrito" runat="server">
										<option value="0">[-Seleccionar-]</option>                                                  
                                     </select>
                                </td>	                                                               
                            </tr>
                            
                            <tr>
                                <td class="label">Tipo de Cuenta</td>
                                <td class="input">
                                    <select id="cmbTipoCuenta" name="cmbTipoCuenta" runat="server">
                                        <option>-Seleccionar-</option>
                                    </select>
                                </td>
                                <td class="label">Moneda</td>
                                <td class="input">
                                    <select id="cmbMoneda" name="cmbMoneda" runat="server" >
                                        <option>-Seleccionar-</option>
                                    </select>
                                </td>
                                <td class="label">Nº Cuenta</td>
                                <td class="input"><input id="txtNumeroCuenta" name="txtNumeroCuenta" type="text"  size="20" value="" runat="server" /></td>
                            </tr>    
                            
                        </table>                                                      
                        
                    </div>
                    
                    
                    <br />
	                 
	                <!-- Inicio Grilla Listado -->
                    <div class="dv_tabla_contenedoraSola">
                        
						<table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
							<tr>
								<td colspan="6" style="text-align: right; padding-right: 5px">
								
									<span id="sp_nuevo">		
										<div id="dv_eliminar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
											runat="server">
											<a href="javascript:fn_eliminarCesionario();">
												<img alt="" src="../../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
													border="0" />
												Eliminar </a>
										</div>
										<div id="dv_editar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
											runat="server">
											<a href="javascript:fn_editarCesionario();">
												<img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
													border="0" />
												Editar </a>
										</div>
										<div id="dv_agregar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
											dir="ltr">
											<a href="javascript:fn_agregarCesionario();">
												<img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
													border="0" />
												Agregar </a>
										</div>     
										<div id="dv_representante" class="dv_img_boton_mini" style="width: 120px; border: 0px solid #ffffff;"
											runat="server">
											<a href="javascript:fn_cargaRepresentantes();">
												<img alt="" src="../../Util/images/ico_mdl_cotizacion.gif" style="width: 16px; height: 16px;"
													border="0" />
												Representantes</a>
										</div>  
									</span>
									
									<span id="sp_editar">										
										<div id="dv_cancelar" class="dv_img_boton_mini" style="width: 80px; border: 0px solid #ffffff;"
											runat="server">
											<a href="javascript:fn_cancelaCesionario();">
												<img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;"
													border="0" />
												Cancelar </a>
										</div> 
										<div id="dv_agregarEditar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
											dir="ltr">
											<a href="javascript:fn_agregarCesionario();">
												<img alt="" src="../../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;"
													border="0" />
												Grabar </a>
										</div>                                  
                                    </span>
                                                                                                       
								</td>
							</tr>
						</table>
                    
                        <table id="jqGrid_lista_A">
                            <tr>
                                <td />
                            </tr>
                        </table>
                        <div id="jqGrid_pager_A">
                        </div>
                        
                    </div>
                    <!-- Fin Grilla -->
                    
										
				</td>
		    </tr>
	    </table>
	    
      </div>
      
      
      
    </form>
</body>
</html>
