<%@ Page Language="VB" ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="false" CodeFile="frmSolicitudDocumentoClienteRegistro.aspx.vb" Inherits="Verificacion_frmSolicitudDocumentoClienteRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
	
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
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmSolicitudDocumentoClienteRegistro.aspx.js"> </script>
	
</head>
<script language="javascript" type="text/javascript">
    document.onkeydown = function(evt) { return (evt ? evt.which : event.keyCode) != 13; }
    document.onkeypress = function(evt) { if ((evt ? evt.which : event.keyCode) == 39) { event.keyCode = 96; return event.keyCode; } }
</script>
<body>    
<form id="frmCotizacionSolicitudDocumentoCliente" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- **************************************************************************************** -->
        <!-- BOTONES DE CABECERA-->
        <!-- **************************************************************************************** -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
	    <tr>
            <td class="icono">
	            <img alt="" src="../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono" />		
            </td>
	        <td class="titulos">
		        <div class="css_lbl_subTitulo" id="lbl_SubTitulo">Verificación :: Solicitud de Documentos</div>
		        <div class="css_lbl_titulo" id="lbl_titulo">Cliente :: Editar</div>
	        </td>
	        <td class="espacio">&nbsp;</td>
	        <td class="botones" >
	            <div id="dvCancelar" class="dv_img_boton">
			        <a href="javascript:fn_cancelar();">
				        <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
				        Volver
			        </a>
		        </div>
		        <div id="dvGuardar" class="dv_img_boton">
			        <a href="javascript:fn_grabar();">
				        <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
				        Guardar
			        </a>
		        </div>
		        <div id="dvCorreo" class="dv_img_boton" style="width:82px;">
			        <a href="javascript:fn_enviarCarta();">
				        <img alt="" src="../Util/images/ico_acc_msgEnviar.gif" border="0" /><br/>
				         Enviar Correo
			        </a>
		        </div>       
	        </td>
	    </tr>
        </table>	

	    <div id="dv_contenedor" class="css_scrollPane" >
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="lineas"></td>			
            </tr>
            <tr>
                <td class="cuerpo">
                <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="titulo css_lbl_tituloContenido">Datos de la Cotización</td>
                    <td class="botones">							
                        <img alt="" src="../Util/images/ico_resize.jpg" style="cursor:pointer" onclick="javascript:fn_doResize();" />
                    </td>
                </tr>
                </table>
                
                <!-- Inicia de Cabecera!-->
                
                <input id="hddPagChecked" name="hddPagChecked" type="hidden"/>
                <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" value="" runat="server" />
	            <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" value="" runat="server" />
	            <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" value="" runat="server" />
	            <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" value="" runat="server" />
	            <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" value="" runat="server" />
                
                <div id="dvDatos" class="dv_tabla_contenedora">
                    <table id="tb_formulario"  border="0" cellpadding="0" cellspacing="3" style="width:100%">
                        <colgroup>                           
                            <col style="width:15.66%" />
                            <col style="width:17.5%"/>                                                       
                            <col style="width:13.5%"/>                                                       
                            <col style="width:17.5%"/>                                                       
                            <col style="width:16.66%"/>                                                       
                            <col style="width:17.5%"/>                                                       
                        </colgroup>
                        <tr>
                            <td class="label" >Nro. Cotización</td>
                            <td class="input">
                                <input id="txtNroCotizacion" type="text" class="css_input_inactivo" value="" size="15" disabled="disabled" readonly="readonly" runat="server" />
                            </td>
                            <td class="label">CU Cliente</td>
                            <td class="input">
                                <input ID="txtCUCliente" name="txtCUCliente" style="width:90px" class="css_input_inactivo" disabled="disabled" readonly="readonly" runat="server" />
                                <input type="button" id="btnRmCliente" style="display:none" runat="server" />
                            </td>
                            <td class="label">Razón Social o Nombre</td>
                            <td class="input">
                                <input id="txtNombreCliente" name="txtNombreCliente" type="text" class="css_input_inactivo" value="" size="40" disabled="disabled" readonly="readonly" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Nº Contrato</td>
                            <td class="input">
                                <input id="txtNumeroContrato" name="txtNumeroContrato" type="text" class="css_input_inactivo" value="" size="15" disabled="disabled" readonly="readonly" runat="server" />
                            </td>
                            <td class="label">Tipo Persona</td>
                            <td class="input">
                                <input id="txtTipoPersona" name="txtTipoPersona" type="text" class="css_input_inactivo" value="" size="30" disabled="disabled" readonly="readonly"  runat="server" />
                            </td>
                            <td></td>
                            <td>
                                <input id="chkTerminoRecepcion" name="chkTerminoRecepcion" type="checkbox" runat="server" disabled="disabled" />
                                <input id="hddTerminoRecepcion" name="hddTerminoRecepcion" type="hidden" value="0" />
                                <input id="hidMensajeCorreo" name="hidMensajeCorreo" type="hidden" value="" runat="server" />
                                <input id="hidListaDocumento" name="hidListaDocumento" type="hidden" value="" />
                                <input id="txtFechaTerminoRecepcion" type="text" class="css_input_inactivo" value="" size="15" disabled="disabled" runat="server" readonly="readonly" />
                            </td>	
                        </tr>
                        <tr>
                            <td class="label">Clasificación del Bien</td>
                            <td class="input">
                                <input id="txtClasificacionBien"  name="txtClasificacionBien" type="text" class="css_input_inactivo" value="" size="40" disabled="disabled" readonly="readonly"  runat="server" />
                            </td>
                            <td class="label">Tipo de Bien</td>
                            <td class="input">
                                <input id="txtTipoInmueble"  name="txtTipoInmueble" type="text" class="css_input_inactivo" value="" size="30" disabled="disabled" readonly="readonly"  runat="server" />
                            </td>
                            <td class="label" >Procedencia</td>
                            <td class="input">
                                <input id="txtProcedencia"  name="txtProcedencia" type="text" class="css_input_inactivo" value="" size="15" disabled="disabled" readonly="readonly" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>  
                
                
                <br/>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="titulo css_lbl_tituloContenido">Datos del Contacto</td>
                    </tr>
                </table>
                <div class="dv_tabla_contenedora">
                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:97%">
                    <colgroup>                           
                        <col class="label" style="width:10%;text-align:right" />
                        <col class="input" style="width:14%"/>                                                       
                        <col class="label" style="width:10%;text-align:right"/>                                                       
                        <col class="input" style="width:14%"/>                                                       
                        <col class="label" style="width:10%;text-align:right"/>                                                       
                        <col class="input" style="width:14%"/>   
                        <col class="label" style="width:10%;text-align:right"/>                                                       
                        <col class="input" style="width:14%"/>                                                      
                    </colgroup>
                    
                    <tr>
                        <td class="label">Contacto</td>
                        <td>
                            <input id="txtContacto" name="txtContacto" type="text" class="css_input" value="" size="30"  runat="server" style="width:200px"/>
                        </td>
                        <td class="label">Cargo</td>
                        <td>
                            <select id="cmbCargo" name="cmbCargo" runat="server">
                            </select>
                        </td>    
                        <td class="label">Correo</td>
                        <td>
                            <input id="txtCorreo" name="txtCorreo" type="text" class="css_input" value="" size="30"  runat="server" style="width:200px"/>
                        </td>
                     </tr>    
                    <tr>   
                    <td class="label">Teléfono</td>
                    <td >
                        <input id="txtTelefonos" name="txtTelefonos" type="text" class="css_input" value="" size="30"  runat="server" style="width:120px"/>
                    </td>
                    <td class="label">Anexo</td>
                    <td>
                        <input id="txtAnexo" name="txtAnexo" type="text" class="css_input" value="" size="30"  runat="server" style="width:100px"/>
                    </td>
                </tr>  
                </table>
                    <asp:HiddenField ID="hidCodContacto" runat="server" />
                </div>
                <br/>
                <table  border="0" cellpadding="0" cellspacing="0">
                    <tr>
                    <td class="titulo css_lbl_tituloContenido">Documentos y Condiciones</td>
                    </tr>
                </table>
                <div id="CotizacionAgregar" class="dv_tabla_contenedora">
                
                    <table  border="0" cellpadding="0" cellspacing="2" style="width:500px;">
                        <tr>            
                            <td class="input" style="height:25px;">
                                <input id="opcioncodumento" name="opcioncodumento" type="radio" checked="checked" onclick="fn_validaTipoDocumento('D')" value="D" />Documento
                                &nbsp;&nbsp;
                                <span id="tr_documento">
                                    <input id="txtnombredocumento" name="txtnombredocumento" type="text" class="css_input" value="" size="50" runat="server" /> 
                                </span>
                                
                            </td>
                            <td rowspan="2">
                                <div id="dvAgregar" class="dv_img_boton_mini" style="border:0">
                                    <a href="javascript:fn_agregar();">
                                        <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width:16px; height:16px;display:inline;" border="0" />Agregar
                                    </a>
                                </div>
                            </td>
                        </tr>  
                        <tr>
                            <td class="input" style="height:25px;">
                                <input id="opcioncodumento" name="opcioncodumento" type="radio" onclick="fn_validaTipoDocumento('C')" value="C" />Condición                
                                &nbsp;&nbsp;
                                <span id="tr_condicion" style="display:none;">
                                    <select id="cmbCondicionesAdicionales" name="cmbCondicionesAdicionales" runat="server">
                                    </select>
                                </span>
                            </td>
                        </tr>            
                    </table>     
                    
                    <table border="0" style="width:900px;">             
                        <tr>
                            <td>
                                <table id="jqGrid_lista_A"><tr><td/></tr></table>
                                <div id="jqGrid_pager_A"></div>  
                            </td>
                        </tr>
                    </table>	
                        
                </div>
                
                </td>
            </tr>		        
            </table>
	    </div>
    </div>
</form>
</body>
</html>
