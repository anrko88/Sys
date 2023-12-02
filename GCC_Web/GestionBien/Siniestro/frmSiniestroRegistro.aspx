<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiniestroRegistro.aspx.vb" Inherits="GestionBien_Siniestro_frmSiniestroRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>GCC :: Sistema de Gestión de Leasing</title>
    
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
    
    <script type="text/javascript" src="../../Util/js/js_global.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.modal.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.date.js"> </script>
    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"> </script>
    <script type="text/javascript" src="../../Util/js/jquery/jquery.dateFormat-1.0.js"></script>
    <script type="text/javascript" src="../../Util/js/jquery/jshashtable.js"></script>    
    <script type="text/javascript" src="../../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->
    <script type='text/javascript' src="frmSiniestroRegistro.aspx.js"> </script>

</head>
<body>

    <form id="frmDemandaRegistro" runat="server">
    
    <input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
    <input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />
    <input type="hidden" name="hddCodSiniestro" id="hddCodSiniestro" value="" runat="server" />
    <input type="hidden" name="hddCodUnico" id="hddCodUnico" value="" runat="server" />
    
    <input type="hidden" name="hddVer" id="hddVer" value="" runat="server" />
    <input type="button" name="btnBuscar" id="btnBuscar" onclick="javascript:fn_buscaSiniestro();" style="display: none;" />
    
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_siniestro.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">Gestión del Bien</div>
                    <div class="css_lbl_titulo">Siniestro :: Registro</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                
					<div id="dv_botonCancelar" class="dv_img_boton">
					    <a href="javascript:fn_cancelar();">
						    <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
						    Volver
					    </a>
				    </div>
                   				    				    
                </td>
            </tr>
        </table>
        

		<input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="NUEVO" runat="server" />
        
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                         
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td class="titulo css_lbl_tituloContenido">Datos del Contrato y Bien</td>
								<td class="botones">							
									<img alt="" src="../../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
								</td>
							</tr>
						</table>
						
                        <div class="dv_tabla_contenedora">
					    
							<table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
								<tr>
									<td class="label">
										Nº Contrato
									</td>
									<td class="input">
										<input id="txtNroContrato" type="text" class="css_input" runat="server" size="10" />
									</td>
									<td class="label">
										CU Cliente
									</td>
									<td class="input">
										<input id="txtCUCliente" type="text" class="css_input" runat="server" size="10" />
									</td>
									<td class="label">
										Razón Social o Nombre
									</td>
									<td class="input" colspan="3">
										<input id="txtRazonSocial" type="text" class="css_input" runat="server" size="40" />
									</td>
								</tr>
								<tr>
									<td class="label">
										Estado del Contrato
									</td>
									<td class="input">
										<input id="txtEstadoContrato" type="text" class="css_input" runat="server" size="25" />
									</td>
									<td class="label">
										Clasificación del Bien
									</td>
									<td class="input">
										<input id="txtClasificacionBien" type="text" class="css_input" runat="server" size="40" />
									</td>
									<td class="label">
										Tipo de Bien
									</td>
									<td class="input" colspan="3">
										<input id="txtTipoBien" type="text" class="css_input" runat="server" size="25" />
									</td>
								</tr>
								<tr>
									<td class="label">
										Moneda
									</td>
									<td class="input">
										<input id="txtMoneda" type="text" class="css_input" runat="server" size="20" />
									</td>
									<td class="label">
										Valor del Bien
									</td>
									<td class="input">
										<input id="txtValorBien" type="text" class="css_input" runat="server" size="10" />
									</td>
									<td class="label">
										F. de Transferencia por OC
									</td>
									<td class="input" colspan="3">
										<input id="txtFecTransferencia" type="text" class="css_input" runat="server" size="12" />
									</td>
								</tr>	
								<tr>
									<td class="label">
										Placa Actual
									</td>
									<td class="input">
										<input id="txtPlaca" type="text" class="css_input" runat="server" size="12" />
									</td>	
									<td class="label">
										Descripción del Bien
									</td>
									<td class="input">
										<input id="txtDescripcion" type="text" class="css_input" runat="server" size="30" />
									</td>	
									<td class="label">
										Ubicación
									</td>
									<td class="input">
										<input id="txtUbicacionCab" type="text" class="css_input" runat="server" size="30" />
									</td>								
								</tr>	
								<tr>									
									<td class="label">
										Ejecutivo Banca
									</td>
									<td class="input">
										<input id="txtEjecutivoBanca" type="text" class="css_input" runat="server" size="30" />
									</td>	
									<td class="label">
										Ejecutivo Leasing
									</td>
									<td class="input">
										<input id="txtEjecutivoLeasing" type="text" class="css_input" runat="server" size="30" />
									</td>								
								</tr>								
							</table>   
																														                                                 
                        </div>
                              
                        <br/> 
                              
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td class="titulo css_lbl_tituloContenido">Listado de Siniestros</td>							
							</tr>
						</table>
						
                        <div class="dv_tabla_contenedora">
                        
							<table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" id="tb_opciones">
                                    <tr>
                                        <td colspan="6" style="text-align: right; padding-right: 5px">
                                            <div id="dv_eliminar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
                                                runat="server">
                                                <a href="javascript:fn_eliminarSiniestro();">
                                                    <img alt="" src="../../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Eliminar </a>
                                            </div>
                                            <div id="dv_editar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
                                                runat="server">
                                                <a href="javascript:fn_editarSiniestro();">
                                                    <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Editar </a>
                                            </div>
                                            <div id="dv_agregar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
                                                dir="ltr">
                                                <a href="javascript:fn_agregarSiniestro();">
                                                    <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Agregar </a>
                                            </div> 
                                            <div id="dv_ver" class="dv_img_boton_mini" style="width: 100px; border: 0px solid #ffffff;"
                                                runat="server">
                                                <a href="javascript:fn_verSiniestro();">
                                                    <img alt="" src="../../Util/images/ico_mdl_cotizacion.gif" style="width: 16px; height: 16px;"
                                                        border="0" />
                                                    Ver Siniestro</a>
                                            </div>                                           
                                        </td>
                                    </tr>
                                </table>
								
							<table id="jqGrid_lista_A">
                                <tr><td /></tr>
                            </table>
                            <div id="jqGrid_pager_A"></div>
                        
                        </div>         
                                                
                    </td>
                </tr>
            </table>
            
                        
        </div>                
        <!-- Fin Cuerpo -->
        
    </div>
    </form>
</body>
</html>
