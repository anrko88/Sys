<%@ Page Language="VB" ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="false" CodeFile="frmPipelineRegistro.aspx.vb" Inherits="Verificacion_frmPipelineRegistro" %>

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
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	<!-- Local -->	
	<script type='text/javascript' src="frmPipelineRegistro.aspx.js"> </script>
	
</head>
<script language="javascript" type="text/javascript">
    document.onkeydown = function(evt) { return (evt ? evt.which : event.keyCode) != 13; }
    document.onkeypress = function(evt) { if ((evt ? evt.which : event.keyCode) == 39) { event.keyCode = 96; return event.keyCode; } }
</script>
<body>    
<form id="frmPipeline" runat="server">
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
	            <img alt="" src="../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono"/>		
            </td>
	        <td class="titulos">
		        <div class="css_lbl_subTitulo" id="lbl_SubTitulo">Seguimiento y Gestión Comercial</div>
		        <div class="css_lbl_titulo" id="lbl_titulo">Pipeline :: Editar</div>
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
				        <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
				        Guardar
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
                <td class="titulo css_lbl_tituloContenido">Datos de la Operación</td>
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
                        <col />
                        <col style="width:17.5%"/>                                                       
                        <col style="width:13.5%"/>                                                       
                        <col style="width:17.5%"/>                                                       
                        <col style="width:16.66%"/>                                                       
                        <col style="width:17.5%"/>                                                       
                    </colgroup>
                    <tr>
                        <td class="label" >Nro. Cotización</td>
                        <td class="input" style="width: 14%">
                            <input id="txtNroCotizacion" type="text" class="css_input_inactivo" value="" size="15" disabled="disabled" readonly="readonly"  runat="server" />
                        </td>
                        <td class="label" >Nº Contrato</td>
                        <td class="input">
                            <input id="txtNumeroContrato"  name="txtNumeroContrato" type="text" class="css_input_inactivo" value="" size="15" disabled="disabled" readonly="readonly"  runat="server" />
                        </td>
                        <td class="label" >Ejecutivo Banca</td>
                        <td class="input">
                            <input id="txtEjecutivoBanca"  name="txtEjecutivoBanca" type="text" class="css_input_inactivo" value="" size="30" disabled="disabled" readonly="readonly"  runat="server" />
                        </td>
                      
                    </tr>
                    <tr>
                        <td class="label" >CU Cliente</td>   
                        <td class="input">                    
                            <input ID="txtCUCliente"  name="txtCUCliente" type="text"  class="css_input_inactivo"  size="15" disabled="disabled" readonly="readonly" runat="server" />
                         
                        </td>
                        <td class="label">Razón Social o Nombre</td>
                        <td class="input">
                            <input id="txtRazonsocial" name="txtRazonsocial" type="text" class="css_input_inactivo" value="" size="40" disabled="disabled" readonly="readonly"  runat="server" />
                        </td>
                       
                        <td class="label">Ejecutivo Leasing</td>
                        <td class="input">                           
                         <input id="txtEjecutivoLeasing" name="txtEjecutivoLeasing" type="text" class="css_input_inactivo" value="" size="30" disabled="disabled" runat="server" readonly="readonly" />
                        </td>	
                    </tr>
                    <!-- Inicio IBK - AAE - 21/10/2012 - Se agregan campos moneda, monto leasing y monto desembolsado -->
                    <tr>
                        <td class="label" >Tipo Leasing</td>   
                        <td class="input">                    
                            <input ID="txtTipo"  name="txtTipo" type="text"  class="css_input_inactivo"  size="20" disabled="disabled" readonly="readonly" runat="server" />                         
                        </td>
                        <td class="label">Clasificación del Bien</td>
                        <td class="input">
                            <input id="txtClasificacionDelBien" name="txtClasificacionDelBien" type="text" size="40" class="css_input_inactivo"
                                readonly="readonly" runat="server" />
                        </td>
                        <td class="label">Tipo de Bien</td>
                        <td class="input">
                            <input id="txtTipoDeBien" name="txtTipoDeBien" type="text" class="css_input_inactivo" size="30"
                                readonly="readonly"  runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label" >Moneda</td>   
                        <td class="input">                    
                            <input ID="TxtMoneda"  name="TxtMoneda" type="text"  class="css_input_inactivo"  size="10" disabled="disabled" readonly="readonly" runat="server" />
                         
                        </td>
                        <td class="label">Banca</td>
                        <td class="input">
                            <input id="txtBanca" name="txtBanca" type="text" class="css_input_inactivo" value="" size="20" disabled="disabled" readonly="readonly"  runat="server" />
                        </td>
                       
                        <td class="label">Precio Venta</td>
                        <td class="input">                           
                         <input id="txtPrecioVenta" name="txtPrecioVenta" type="text" class="css_input_inactivo" value="" size="20" disabled="disabled" runat="server" readonly="readonly" />
                        </td>	
                    </tr>
                    <tr>
                        <td class="label" >Monto Leasing</td>   
                        <td class="input">                    
                            <input id="TxtMontoLeasing" name="txtMontoLeasing" type="text" class="css_input_inactivo" value="" size="20" disabled="disabled" readonly="readonly"  runat="server" />
                         
                        </td>
                        <td class="label">Riesgo Neto</td>
                        <td class="input">
                            <input id="txtRiesgoNeto" name="txtRiesgoNeto" type="text" class="css_input_inactivo" value="" size="20" disabled="disabled" readonly="readonly"  runat="server" />
                        </td>
                       
                        <td class="label">Monto Desembolsado</td>
                        <td class="input">                           
                         <input id="TxtMontoDesembolsado" name="TxtMontoDesembolsado" type="text" class="css_input_inactivo" value="" size="20" disabled="disabled" runat="server" readonly="readonly" />
                        </td>	
                    </tr>
                    <!-- Fin IBK - AAE -->
                </table>
            </div>  
            
            
            <br/>
            <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titulo css_lbl_tituloContenido">Estado</td>
            </tr>
            </table>
            <div class="dv_tabla_contenedora">
            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:97%">
            <colgroup>                           
               <col />
                        <col style="width:17.5%"/>                                                       
                        <col style="width:13.5%"/>                                                       
                        <col style="width:17.5%"/>                                                       
                        <col style="width:16.66%"/>                                                       
                        <col style="width:17.5%"/>                                                      
            </colgroup>
            <tr>
                <td class="label">Estado</td>
                <td>
                    <select id="cmbEstado" name="cmbEstado" runat="server" onchange="fn_OcultarMostrarPorcentajeDesembolso(this.value);">
                    <option value="0">- Seleccionar -</option>
                    </select>
                </td>
                <td class="label">Motivo Demora</td>
                <td>
                    <select id="cmbMotivoDemora" name="cmbMotivoDemora" runat="server">
                    <option value="0">- Seleccionar -</option>
                    </select>
                </td>  
                <!-- Inicio IBK - AAE - 21/10/2012 - Se agrega campo comentario -->
                <td>
                    <td class="label">Comentario:</td>                    
                </td> 
                <td>
                    <input id="TxtComentario" name="TxtComentario" type="text" class="css_input" value="" size="50"  runat="server" />  
                </td>
                <!-- Fin IBK - AAE -->
             
            </tr>  
        
           
          
            <asp:HiddenField ID="hidCodCotizacion" runat="server" />
            </table>
            </div>
            
            <br/>
            
            <div id="dv_desembolso" runat="server">
                <table  border="0" cellpadding="0" cellspacing="0">
            <tr>
            <td class="titulo css_lbl_tituloContenido">Desembolso</td>
            </tr>
            </table>
            
            <div class="dv_tabla_contenedora">
            <table id="Table1" border="0" cellpadding="0" cellspacing="3" style="width:97%">
         
            <tr>
                <td class="label">Anterior</td>
                <td>
                 <input id="txtAnterior" name="txtAnterior" type="text" class="css_input" value="" size="20"  runat="server" />%
                </td>
                <td class="label">Mes Actual</td>
                <td>
                  <input id="txtActual" name="txtActual" type="text" class="css_input" value="" size="20"  runat="server" />%
                </td>    
                <td class="label">Siguiente Mes</td>
                <td>
                    <input id="txtsiguienteMeses" name="txtsiguienteMeses" type="text" class="css_input" value="" size="20"  runat="server" />%
                </td>
                 <td class="label">Siguiente Año</td>
                <td>
                    <input id="txtsiguienteAnios" name="txtsiguienteAnios" type="text" class="css_input" value="" size="20"  runat="server" />%
                </td>
             </tr>             
            
            </table>
            </div>
            </div>
            
            
          
            
            </td>
        </tr>		        
        </table>
	    </div>
    </div>
</form>
</body>
</html>
