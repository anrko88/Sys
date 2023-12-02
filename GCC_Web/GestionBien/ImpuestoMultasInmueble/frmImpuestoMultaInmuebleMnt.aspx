<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImpuestoMultaInmuebleMnt.aspx.vb" Inherits="GestionBien_ImpuestoMultasInmueble_frmImpuestoMultaInmuebleMnt" %>

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

	<!-- Local -->	
	<script type='text/javascript' src="frmImpuestoMultaInmuebleMnt.aspx.js"></script>
	
</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
		
		<input type="hidden" name="hddTipoTx" id="hddTipoTx" value="" runat="server" />
		<input type="hidden" name="hddVer" id="hddVer" value="" runat="server" />
    
		<input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
		<input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />		
		<input type="hidden" name="hddCodUnico" id="hddCodUnico" value="" runat="server" />
		<input type="hidden" name="hddCodImpuesto" id="hddCodImpuesto" value="" runat="server" />
		
		<input type="hidden" name="hddPagoCliente" id="hddPagoCliente" value="" runat="server" />
		<input type="hidden" name="hddEstadoPago" id="hddEstadoPago" value="" runat="server" />
		<input type="hidden" name="hddEstadoCobro" id="hddEstadoCobro" value="" runat="server" />
		<input type="hidden" name="hddMunicipalidad" id="hddMunicipalidad" value="" runat="server" />
		<input type="hidden" name="hddFechaTransferencia" id="hddFechaTransferencia" value="" runat="server" />
		
		<input type="hidden" name="hddTotalPredial" id="hddTotalPredial" value="" runat="server" />
		<input type="hidden" name="hddTotalAutovaluo" id="hddTotalAutovaluo" value="" runat="server" />		
		<input type="hidden" name="hddNroLote" id="hddNroLote" value="" runat="server" />		
		<%--Inicio IBK - AAE - Variable para ver si tengo el nro del lote asignado --%>    
        <input type="hidden" id="hidReadOnly" name="hidReadOnly" value="" runat="server" />    
        <input id="hidTengoLote" type="hidden" runat="server" />
        <input type="hidden" name="hddPerfil" id="hddPerfil" value="" runat="server" />
        <%--Fin IBK - AAE--%>
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <div id="dv_cuerpoModal">
            <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
    	    <tr>
			    <td class="icono">
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Util/images/ico_mdl_impuesto.gif" />
                </td>
			    <td class="titulos" style="width:350px;">
				    <div class="css_lbl_subTitulo">Gestión del Bien</div>
				    <div class="css_lbl_titulo">Impuestos y Multas Inmuebles :: Mantenimiento</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
			    
    				    <div id="dv_img_boton" class="dv_img_boton">
					        <a href="javascript:parent.fn_util_CierraModal();">
						        <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						        Cerrar
					        </a>
				        </div>
			    	    <div id="dv_botonGuardar" class="dv_img_boton">
					        <a href="javascript:fn_guardar();">
						        <img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0"/><br />
						        Guardar
					        </a>
				        </div>
				         
						<div id="dv_separador" class="dv_img_boton_separador">
							:
						</div>
                    		        
						<div id="dv_btnDocumentos" class="dv_img_boton" style="width:80px;">
							<a href="javascript:fn_GBAbreDocumentos();">
								<img alt="" src="../../Util/images/ico_version.gif" border="0"/><br />
								Documentos
							</a>
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
										
					<div class="dv_tabla_contenedora" style="padding-top: 0px;">
                                                
                        <fieldset>
							<legend class="css_lbl_subTitulo">Datos del Bien</legend>
					                  
					        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                        
								<tr>
									<td class="label">
										Nro. Contrato
									</td>
									<td class="input">
										<input id="txtNroContrato" type="text" class="css_input" runat="server" size="12" />
									</td>                                
									<td class="label">
										CU Cliente
									</td>
									<td class="input">
										<input id="txtCUCliente" type="text" class="css_input" runat="server" size="12" />
									</td>                                
									<td class="label">
										Tipo de Documento
									</td>
									<td class="input">
										<input id="txtTipoDocumento" type="text" class="css_input" runat="server" size="15" />
									</td>                                
								</tr>          
								   
								<tr>
									<td class="label">
										Nro. de Documento
									</td>
									<td class="input">
										<input id="txtNroDocumento" type="text" class="css_input" runat="server" size="15" />
									</td>                                
									<td class="label">
										Razón Social o Nombre
									</td>
									<td class="input" colspan="3">
										<input id="txtRazonSocial" type="text" class="css_input" runat="server" size="50" />
									</td>                                									                            
								</tr> 
								   
								<tr>
									<td class="label">
										Departamento
									</td>
									<td class="input">
										<input id="txtDepartamento" type="text" class="css_input" runat="server" size="12" />
									</td>                                
									<td class="label">
										Provincia
									</td>
									<td class="input">
										<input id="txtProvincia" type="text" class="css_input" runat="server" size="15" />
									</td>                                
									<td class="label">
										Distrito
									</td>
									<td class="input">
										<input id="txtDistrito" type="text" class="css_input" runat="server" size="15" />
									</td>                                
								</tr>    
								
								<tr>
									<td class="label">
										Dirección
									</td>
									<td class="input" colspan="3">
										<input id="txtUbicacion" type="text" class="css_input" runat="server" size="70" />
									</td>
									<td class="label" id="id_loteLabel">
										Lote
									</td>
									<td class="input" id="id_loteInput">
										<input id="txtLote" type="text" class="css_input" runat="server" size="10" />
										<input id="txtmuni" type="text" style="display:none" runat="server" />
									</td>  
								</tr> 
								   
					        </table>    
					                    
                        </fieldset>
                        
                        
                        <fieldset>
							<legend class="css_lbl_subTitulo">Datos del Impuesto y Multa</legend>
					        
					        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                        
								<tr>
									<td class="label">
										Periodo
									</td>
									<td class="input">
										<input id="txtPeriodo" type="text" class="css_input" runat="server" size="8" />
									</td>
									<td class="label">
										Total Autovaluo
									</td>
									<td class="input">
										<input id="txtTotalAutovaluo" type="text" class="css_input" runat="server" size="12"  style="text-align:right" />
									</td>                                
									<td class="label">
										Total Predial 
									</td>
									<td class="input">
										<input id="txtTotalPredial" type="text" class="css_input" runat="server" size="12"  style="text-align:right" />
									</td>                                									                            
								</tr>          
								   
								<tr>									                                
									<td class="label">
										Cod. Predio
									</td>
									<td class="input">
										<input id="txtCodPredio" type="text" class="css_input" runat="server" size="10" />
									</td>  
									<td class="label">
										Autovaluo
									</td>
									<td class="input">
										<input id="txtAutovaluo" type="text" class="css_input" runat="server" size="12" style="text-align:right" />
									</td> 
									<td class="label">
										Impuesto Predial
									</td>
									<td class="input">
										<input id="txtImpuestoPredial" type="text" class="css_input" runat="server" size="12"  style="text-align:right" />
									</td>                                   									                            
								</tr> 
								   
								<tr>									                           
									<td class="label">
										Arbitrio
									</td>
									<td class="input">
										<input id="txtArbitrio" type="text" class="css_input" runat="server" size="12"  style="text-align:right" />
									</td>                                
									<td class="label">
										Multa
									</td>
									<td class="input">
										<input id="txtMulta" type="text" class="css_input" runat="server" size="12"  style="text-align:right" />
									</td>									  
									<td class="label">
										Fiscalización
									</td>
									<%--Inicio JJM IBK--%>
									<td class="input" >
									    <input type="checkbox" id="chkFizcalizacion" class="input" runat="server" />
									</td> 									
									<td class="input">
										<input id="txtFiscalizacion"  visible="false" type="text" class="css_input" runat="server" size="12"  style="text-align:right" />
									</td>                                
									<%--Fin JJM IBK--%>
								</tr>    								
								<tr>									                             
									<td class="label">
										Importe Total
									</td>
									<td class="input">
										<input id="txtTotal" type="text" class="css_input" runat="server" size="15" />
									</td>
									<%-- Inicio IBK - AAE - Variable para ver si tengo el nro del lote asignado --%>
                                    <td class="label">
                                        Cobro Adelantado
                                    </td>
                                    <td class="input">
                                        <input id="cbCobroAdelantado" type="checkbox" runat="server" />
                                    </td>                                 
                                    <td class="label">
                                        No Aplicar Comisión
                                    </td>
                                    <td class="input">
                                        <input id="cbNoComision" type="checkbox" runat="server" />
                                    </td>                                 
                                    <%-- Fin IBK --%>                                									
								</tr>  
								
								<tr>
									<td class="label">
										Pago Cliente
									</td>
									<td class="input">
										<input id="chkPagoCliente" name="chkPagoCliente" type="checkbox" runat="server" />
									</td>                        
									<td class="label">
										F. Pago
									</td>
									<td class="input">
										<input id="txtFechaPago" type="text" class="css_input" runat="server" size="12" />
									</td>                                
									<td class="label">
										Estado Pago
									</td>
									<td class="input">
										<input id="txtEstadoPago1" type="text" class="css_input" runat="server" size="15" style="display:none;" />
										<select id="txtEstadoPago" name="txtEstadoPago" runat="server">
                                        </select>
									</td>                                									
								</tr>  
								
								<tr>
									<td class="label">
										F. Cobro
									</td>
									<td class="input">
										<input id="txtFecCobro" type="text" class="css_input" runat="server" size="12" />																			
									</td>                                
									<td class="label">
										Estado Cobro
									</td>
									<td class="input">
										<input id="txtEstadoCobro1" type="text" class="css_input" runat="server" size="15" style="display:none;" />
										<select id="txtEstadoCobro" name="txtEstadoCobro" runat="server">
                                        </select>	
									</td>                                									
								</tr>   
								
								<tr>
									<td class="label">
										Observación
									</td>
									<td class="input" colspan="5">
										<textarea name="txtObservacion" id="txtObservacion" cols="90" rows="2" runat="server"></textarea>
									</td>
								</tr> 
								   
					        </table>            
					                    
                        </fieldset>
                        
                    </div>
										
				</td>
		    </tr>
	    </table>
	    
      </div>
      
      
      
    </form>
</body>
</html>
