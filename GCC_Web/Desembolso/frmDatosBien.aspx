<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDatosBien.aspx.vb" Inherits="Desembolso_frmDatosBien" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
	
	<script type="text/javascript" src="../Util/js/js_global.js"></script>
	<script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>
	<script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>	
	<script type='text/javascript' src="../Util/js/js_util.date.js"> </script>	
		
	<!-- JQGrid -->		
	<link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../Util/componentes/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>
	<script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>	

    <!-- Local -->
    <script src="frmDatosBien.aspx.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="frmDatosBien" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
	        <tr>
		        <td class="icono">
				    <img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono"/></td>
		        <td class="titulos">
		            <div class="css_lbl_subTitulo">Desembolso</div>
		            <div class="css_lbl_titulo">Desembolso :: Modificación Bien</div>
		        </td>
		        <td class="espacio">&nbsp;</td>
		        <td class="botones">
				    <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:parent.fn_util_CierraModal();">
					        <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
					        Cancelar
				        </a>
			        </div>
			        
				    <div id="Div1" class="dv_img_boton_separador">
	    		        :
    			    </div>
		
		    	    <div id="dv_img_boton" class="dv_img_boton">
				        <a href="javascript:parent.fn_util_CierraModal();">
					        <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
					        Guardar
				        </a>
			        </div>
		    </td>
	        </tr>
        </table>	
	    <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
	    </table>
	    
         <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
	        <tr>
		        <td class="titulo css_lbl_tituloContenido">
		            <div id="dvContrato">Nro Contrato:&nbsp;&nbsp;0003</div>
		            <div id="dvRegistro">Registro</div>
		        </td>
	        </tr>
        </table>

	    <!-- *********************************************************************
              Mantenimiento de Bienes
             *********************************************************************
         -->			        			    
        <div id="dv_datos_inmueble" class="dv_tabla_contenedora">
	        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
		        <tr>
			        <td class="label" style="width:16.66;">Descripción</td>
			        <td class="input" style="width:16.66;"><input id="txtDescripcionDemanda" type="text" class="css_input" size="50" value="" /></td>	
			        <td class="label" style="width:16.66;">Partida Registral</td>
		            <td class="input" style="width:16.66;"><input id="Text3" type="text" class="css_input" size="18" value="" /></td>
                    <td style="width:16.66;">&nbsp;</td>
                    <td style="width:16.66;">&nbsp;</td>
			    </tr>    
			    <tr>
			        <td class="label">Departamento</td>
			        <td class="input">
			            &nbsp;<select id="Select4" name="D2">
                            <option selected="selected">- Seleccione -</option>
                            <option>Lima</option>                                        
                            <option>Ica</option>                                        
                            <option>Tacna</option>                                        
                        </select></td>	
			        <td class="label">Provincia</td>
			        <td class="input">
			            <select id="Select3">
                            <option selected="selected">- Seleccione -</option>
                            <option>Lima</option>
                            <option>Callao</option>
                        </select>
			        </td>
			        <td class="label">Distrito</td>
			        <td class="input">
			            <select id="Select2" name="D1" >
                            <option selected="selected">- Seleccione -</option>
                            <option>Ate</option>
                            <option>San Borja</option>
                            <option>La Molina</option>
                            <option>Cercado</option>
                            <option>San Isidro</option>
                        </select>
			        </td>							    
		        </tr>
		        <tr>
			        <td class="label">Tipo Inmueble</td>
			        <td class="input">
                        <select id="Select1" style="width:200px;">
                            <option selected="selected">- Seleccione -</option>
                            <option>Bien Futuro</option>
                            <option>Compra Venta</option>
                            <option>Edificación</option>
                        </select>
			        </td>	
			        <td class="label">Valor del Bien</td>
		             <td class="input"><input id="txtValorBien" type="text" class="css_input" size="11" value="" /></td>						    
		        </tr>
		        <tr>
		             <td class="label">Fecha de Transferencia</td>
		             <td class="input" colspan="5"><input id="txtFechatransferencia" type="text" class="css_input" size="11" value="" /></td>
		        </tr>
		        <tr>
		            <td class="label">RUC</td>
		            <td class="input"><input id="Text6" type="text" class="css_input" size="18" value="" /></td>
		            <td class="label">Proveedor</td>
		            <td class="input" colspan="3"><input id="Text7" type="text" class="css_input" value="" style="width:100%;" /></td>
		        </tr>
		        <tr>
		            <td class="label">Observaciones</td>
		            <td class="input" colspan="5"><textarea id="TextArea1" cols="20" rows="2" style="width:100%;"></textarea></td>
		        </tr>						    
	        </table>
        </div>
	    
        <!-- *********************************************************************
             Mantenimiento de Vehículo
             *********************************************************************
        -->
        <div id="dv_datos_vehiculo" class="dv_tabla_contenedora">				
            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">                                                          
              <tr>
                    <td class="label">Marca</td>
                    <td class="input"><input id="txtContrato3" type="text" class="css_input" size="15" /></td>
                    <td class="label">Modelo</td>
                    <td class="input"><input id="txtContrato4" type="text" class="css_input" size="15" /></td>
                    <td colspan="2" rowspan="2">
                        <fieldset>
                            <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">Placa actual</td>
                                <td class="input"><input id="txtContrato11" type="text" class="css_input" size="15" /></td>
                            </tr>
                            <tr>
                                <td class="label">Placa anterior</td>
                                <td class="input"><input id="txtContrato12" type="text" class="css_input" size="15" /></td>
                            </tr>
                            </table>
                        </fieldset>
                   </td>
              </tr>
              <tr>
                <td class="label">N° Motor</td>
                <td class="input"><input id="txtContrato1" type="text" class="css_input" size="15" /></td>
                <td class="label">N° Serie</td>
                <td class="input"><input id="txtContrato7" type="text" class="css_input" size="15" /></td>
              </tr>
              <tr>
                <td class="label">Año Fabricación</td>
                <td class="input">
                    <input id="Text4" type="text" class="css_input" size="15" />
                </td>
                <td class="label">Color</td>
                <td class="input"><input id="txtContrato8" type="text" class="css_input" size="15" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td class="label">RUC</td>
                <td class="input">
                    <input id="txtContrato10" type="text" class="css_input" size="15" /></td>
                <td class="label">Proveedor</td>
                <td class="input" colspan="3"><input id="txtRazonSocialProveedor" type="text" size="80" readonly="readonly" disabled="disabled" value="PANA AUTOS S.A." /></td>
              </tr>
              <tr>
                <td class="label">Ubicación</td>
                <td class="input" colspan="5">
                    <textarea id="TextArea4" cols="20" rows="2" style="width:690px;" name="S1"></textarea>
                </td>
              </tr>
              </table>
        </div>
        
        <!-- *********************************************************************
             Mantenimiento Otros
             *********************************************************************
        -->
        <div id="dv_datos_otros" class="dv_tabla_contenedora">
	        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">                               
                <tr>
                    <td class="label" style="width:15%;">Cantidad</td>
                    <td class="input" style="width:35%;"><input id="Text10" type="text" class="css_input" size="12"/></td>
                    <td class="label" style="width:15%;">Descripción</td>
                    <td class="input" style="width:35%;"><textarea name="textarea" id="textarea2" cols="55" rows="2" style="width:100%;"></textarea></td>
                  </tr>
                <tr>
                    <td class="label">RUC</td>
                    <td class="input">
                        <input id="Text12" type="text" class="css_input" size="12" /></td>
                    <td class="label">Proveedor</td>
                    <td class="input"><input id="Text13" type="text" class="css_input" value="" style="width:100%;" /></td>
                  </tr>
                <tr>
                    <td class="label">Ubicación</td>
                    <td class="input" colspan="3"><textarea id="TextArea5" cols="20" rows="2" style="width:100%;" name="S2"></textarea></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <fieldset>
                        <legend>Datos adicionales</legend>
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%; text-align:left;">
                                <tr>
                                    <td class="label" style="text-align:left;">Observaciones</td>                                                        
                                    <td class="input"><textarea name="textarea" id="textarea3" cols="55" rows="4" style="width:100%;"></textarea></td>
                                </tr>
                            </table>
                        </fieldset>
                   </td>
                </tr>
            </table>
        </div>  
          
    </div>	
    </form>
</body>
</html>
