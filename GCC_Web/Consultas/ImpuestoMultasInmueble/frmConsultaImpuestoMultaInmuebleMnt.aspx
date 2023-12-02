<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaImpuestoMultaInmuebleMnt.aspx.vb" Inherits="Consultas_ImpuestoMultasInmueble_frmConsultaImpuestoMultaInmuebleMnt" %>

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
	<script type='text/javascript' src="frmConsultaImpuestoMultaInmuebleMnt.aspx.js"></script>	
	
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
		
		<input type="hidden" name="hddFechaTransferencia" id="hddFechaTransferencia" value="" runat="server" />
				
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
				    <div class="css_lbl_subTitulo">Consultas</div>
				    <div class="css_lbl_titulo">Impuestos y Multas Inmuebles :: Consulta</div>
			    </td>
			    <td class="espacio">&nbsp;</td>
			    <td class="botones">
			    
    				    <div id="dv_img_boton" class="dv_img_boton">
					        <a href="javascript:parent.fn_util_CierraModal();">
						        <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						        Cerrar
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
							<legend class="css_lbl_subTitulo">Consultas</legend>
					                  
					        <table id="tb_formulario"  border="0" cellpadding="0" cellspacing="3">
					            <colgroup>
					                <col width="16%" />
					                <col width="16%" />
					                <col width="16%" />
					                <col width="16%" />
					                <col width="16%" />
					                <col width="16%" />
					            </colgroup>
								<tr>
									<td class="label">Nro. Contrato</td>
									<td id="txtNroContrato" runat="server"></td>                                
									<td class="label">CU Cliente</td>
									<td id="txtCUCliente" runat="server"></td>                                
									<td class="label">Tipo de Documento</td>
									<td id="txtTipoDocumento" runat="server"></td>                                
								</tr>          
								   
								<tr>
									<td class="label">Nro. de Documento</td>
									<td class="input" id="txtNroDocumento" runat="server"></td>                                
									<td class="label">Razón Social o Nombre</td>
									<td id="txtRazonSocial" runat="server" colspan="3"></td>                                									                            
								</tr> 
								   
								<tr>
									<td class="label">Departamento</td>
									<td id="txtDepartamento" runat="server" ></td>                                
									<td class="label">Provincia</td>
									<td id="txtProvincia" runat="server" ></td>                                
									<td class="label">Distrito</td>
									<td id="txtDistrito" runat="server" ></td>                                
								</tr>    
								
								<tr>
									<td class="label">Ubicación</td>
									<td id="txtUbicacion" runat="server" colspan="3"></td>
									<td class="label" id="id_loteLabel">Lote</td>
									<td id="txtLote" runat="server"></td>  
								</tr> 
								   
					        </table>    
					                    
                        </fieldset>
                        
                        
                        <fieldset>
							<legend class="css_lbl_subTitulo">Datos del Impuesto y Multa</legend>
					        
					        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
					            <colgroup>
					                <col width="16%" />
					                <col width="16%" />
					                <col width="16%" />
					                <col width="16%" />
					                <col width="16%" />
					                <col width="16%" />
					            </colgroup>
								<tr>
									<td class="label">Periodo</td>
									<td id="txtPeriodo" runat="server" ></td>
									<td class="label">Total Autovaluo</td>
									<td id="txtTotalAutovaluo" runat="server" style="text-align:right;padding-right: 50px" ></td>                                
									<td class="label">Total Predial</td>
									<td id="txtTotalPredial" runat="server" style="text-align:right;padding-right: 50px"></td>                                									                            
								</tr>          
								   
								<tr>									                                
									<td class="label">Cod. Predio</td>
									<td id="txtCodPredio" runat="server" ></td>  
									<td class="label">Autovaluo</td>
									<td id="txtAutovaluo" runat="server" style="text-align:right;padding-right: 50px"></td> 
									<td class="label">Impuesto Predial</td>
									<td id="txtImpuestoPredial" runat="server" style="text-align:right;padding-right: 50px"></td>                                   									                            
								</tr> 
								   
								<tr>									                           
									<td class="label">Arbitrio</td>
									<td id="txtArbitrio" runat="server" ></td>                                
									<td class="label">Multa</td>
									<td id="txtMulta" runat="server" style="text-align:right;padding-right: 50px" ></td>   
									<td class="label">Fiscalización</td>
									<td id="txtFiscalizacion" runat="server" style="text-align:right;padding-right: 50px"></td>                                
								</tr>    
								
								<tr>									                             
									<td class="label">Importe Total</td>
									<td id="txtTotal" runat="server"></td>
									<td id="Td1" runat="server" colspan="3"></td>
								</tr>  
								
								<tr>
									<td class="label">Pago Cliente</td>
									<td >
										<input id="chkPagoCliente" name="chkPagoCliente" type="checkbox" runat="server" disabled="True" />
									</td>                        
									<td class="label">F. Pago</td>
									<td id="txtFechaPago" runat="server" ></td>                                
									<td class="label">Estado Pago</td>
									<td id="txtEstadoPago" runat="server"></td>                                									
								</tr>  
								
								<tr>
									<td class="label">F. Cobro</td>
									<td id="txtFecCobro" runat="server" ></td>                                
									<td class="label">Estado Cobro</td>
									<td id="txtEstadoCobro" runat="server" colspan="3">
									</td>                                									
								</tr>   
								
								<tr>
									<td class="label" style="vertical-align: top">Observación</td>
									<td colspan="5" id="txtObservacion" runat="server" style="height: 30px;vertical-align: top;"></td>
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
