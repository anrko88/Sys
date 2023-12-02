<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCesionContratoRegistro.aspx.vb" Inherits="Formalizacion_CesionContrato_frmCesionContratoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SGL :: Sistema de Gestión de Leasing</title>
    
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
    <script type='text/javascript' src="frmCesionContratoRegistro.aspx.js"> </script>

</head>
<body>

    <form id="frmSiniestroListado" runat="server">
    
    <input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
    <input type="hidden" name="hddCodCesionario" id="hddCodCesionario" value="" runat="server" />
    <input type="hidden" name="hddCodContratoTemp" id="hddCodContratoTemp" value="" runat="server" />
    <input type="hidden" name="hddVer" id="hddVer" value="" runat="server" />
    <input type="hidden" name="hddCodEstadoContrato" id="hddCodEstadoContrato" value="" runat="server" />
    <input type="hidden" name="hddCodCesionarioPri" id="hddCodCesionarioPri" value="" runat="server" />
    
    <input type="hidden" name="hddNomCesionario" id="hddNomCesionario" value="" runat="server" />
    <input type="hidden" name="hddCodUnicoCesionario" id="hddCodUnicoCesionario" value="" runat="server" />
        
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_cesion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">Formalización</div>
                    <div class="css_lbl_titulo">Cesión de Contrato :: Registro</div>
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
				    
				    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    
				    <div id="dv_RealizarCesion" class="dv_img_boton" style="width:100px;">
                        <a href="javascript:fn_realizaCesion();">
                            <img alt="" src="../../Util/images/ico_acc_ejecutarWIO.gif" border="0" /><br />
                            Realizar Cesión </a>
                    </div>
                    
                    <div id="dv_documentos" class="dv_img_boton" style="width:80px;">
				        <a href="javascript:fn_GBAbreDocumentos();">
					        <img alt="" src="../../Util/images/ico_version.gif" border="0"/><br />
					        Documentos
				        </a>
			        </div>
                    
                </td>
            </tr>
        </table>
        
        
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                           
                        <div class="dv_tabla_contenedora" style="padding-top:0px;">
                        
							<fieldset>
								<legend class="css_lbl_subTitulo">Datos del Contrato</legend>
									
								<table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
									<tr>
										<td class="label">
											Nro. Contrato
										</td>
										<td class="input">
											<input id="txtNroContrato" type="text" class="css_input_inactivo" size="15" runat="server" />
										</td>                                    
										<td class="label">
											CU Cliente
										</td>
										<td class="input">
											<input id="txtCUCliente" type="text" class="css_input_inactivo" size="15" runat="server" />
										</td> 
										<td class="label">
											Razón Social o Nombre
										</td>
										<td class="input">
											<input id="txtRazonSocial" type="text" class="css_input_inactivo" size="50" runat="server" />
										</td>                                   
									</tr>
									<tr>
										<td class="label">
											Tipo Documento
										</td>
										<td class="input">
											<input id="txtTipoDoc" type="text" class="css_input_inactivo" size="15" runat="server" />
										</td>
										<td class="label">
											Nº Documento
										</td>
										<td class="input">
											<input id="txtNroDocumento" type="text" class="css_input_inactivo" size="15" runat="server" />
										</td>
										<td class="label">
											Clasificación del Bien
										</td>
										<td class="input">
											<input id="txtClasificacionBien" type="text" class="css_input_inactivo" size="50" runat="server" />
										</td>
									</tr>                                
									<tr>
										<td class="label">
											Estado del Contrato
										</td>
										<td class="input">
											<input id="txtEstadoContrato" type="text" class="css_input_inactivo" size="15" runat="server" />
										</td>                                    										                               
									</tr>                                   
								</table>   
								
							</fieldset>
								         
								         
							<fieldset>
								<legend class="css_lbl_subTitulo">Datos del Cesionario</legend>
								
								<div style="width:100%; text-align:right;display:none;">
									<div id="dv_eliminar" class="dv_img_boton_mini" style="width: 110px; border: 0px solid #ffffff;" runat="server">
										<a href="javascript:fn_cargaRepresentantes();">
											<img alt="" src="../../Util/images/ico_mdl_cotizacion.gif" style="width: 16px; height: 16px;" border="0" />
											Representantes
										</a>
									</div>	
								</div>
								
								<!-- Inicio Grilla Listado -->
								<table id="jqGrid_lista_A">
									<tr>
										<td />
									</tr>
								</table>
								<div id="jqGrid_pager_A">
								</div>
								<!-- Fin Grilla -->	
								
							</fieldset>
							
							<fieldset>
								<legend class="css_lbl_subTitulo">Datos de Representantes por Cesionario</legend>
								<br/>
								<!-- Inicio Grilla Listado -->
								<table id="jqGrid_lista_B">
									<tr>
										<td />
									</tr>
								</table>
								<div id="jqGrid_pager_B">
								</div>
								<!-- Fin Grilla -->	
								
							</fieldset>
							
																						                                                   
                        </div>
		                                        
                    </td>
                </tr>
            </table>
                                    
        </div>               
        
    </div>
    <!-- Fin Cuerpo -->
     
    </form>
    
</body>
</html>
