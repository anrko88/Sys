<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTasacionRegistroDetalle.aspx.vb" Inherits="GestionBien_Tasacion_frmTasacionRegistroDetalle" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Observación </title>
    
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
	
	
	<script src="../../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>
    <script src="../../Util/js/jquery/jshashtable.js" type="text/javascript"></script>
   
    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>

	<!-- JQGrid -->		
	<script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>
	<link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />	 
	<script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
	<script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	
	
	<!-- Local -->	
	<script type='text/javascript' src="frmTasacionRegistroDetalle.aspx.js"> </script>
	
</head>
<body>
    <form id="frmTasacionRegistroDetalle" runat="server">
     <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
	    <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width:100%">
	        <tr>
			                <td class="icono">
				                <img src="../../Util/images/ico_mdl_multa.gif" class="jd_menu_icono"/>		
			                </td>
			                <td class="titulos">
				                <div class="css_lbl_subTitulo"> </div>
				                <div class="css_lbl_titulo">Detalle Tasación</div>
			                </td>
			                <td class="espacio">&nbsp;
                								
			                </td>
			                <td class="botones">
                			
    				            <div id="dv_img_boton" class="dv_img_boton">
					                <a href="javascript:parent.fn_util_CierraModal();">
						                <img src="../../Util/images/ico_acc_cancelar.gif" border="0"/><br />
						                Cerrar
					                </a>
				                </div>
            				    
				                <div id="Div3" class="dv_img_boton_separador">
				                    :
				                </div>
            				    
            				    <div id="dv_guardar" class="dv_img_boton">
				                    <div id="dv_img_boton" class="dv_img_boton">
					                    <a href="javascript:fn_guardar();">
						                    <img src="../../Util/images/ico_acc_grabar.gif" border="0"/><br />
						                    Guardar
					                    </a>
				                    </div>	
				                </div>
                			
                				<div id="dv_img_boton" class="dv_img_boton" style="width:80px;display:none;">
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
	    </table>		    
        <br>
        
            <div id="dvDatos">    
            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0">
            <tr>
                    <td class="label">Valor Realización  </td>
                    <td class="input">
                               <input id="txtvalorrealizacion" name="txtvalorrealizacion" type="text" class="css_input" runat="server" />
                    </td>
                    <td class="label">Valor Comercial  </td>
                    <td class="input">
                               <input id="txtValorComercial" name="txtValorComercial" type="text" class="css_input" runat="server" />
                    </td>
            </tr>
            <tr>
                    <td class="label">Fecha Asignación </td>
                    <td class="input">
                               <input id="txtFechaasignacion" name="txtFechaasignacion" type="text" class="css_input" runat="server" />
                    </td>
                    <td class="label">Fecha Tasación  </td>
                    <td class="input">
                               <input id="txtFechatasacion" name="txtFechatasacion" type="text" class="css_input" runat="server" />
                    </td>
            </tr>
            
            <tr>
                    <td class="label">Tasador Asiganado</td>
                    <td class="input">
                        <select id="cmbTasador" name="cmbTasador" runat="server">
                            <option value="000">[-Seleccionar-]</option> 
                        </select>
                    </td>
            </tr>
            </table>
            </div>   
   
    </div>
        
    
        <input type="hidden" id="hddCodContrato" name="hddCodContrato" value="" runat="server" />
        <input type="hidden" id="hddCodtasacion" name="hddCodtasacion" value="" runat="server" />
        <input type="hidden" id="hddCodTasador" name="hddCodTasador" value="" runat="server" />
        <input type="hidden" id="hddSecfinanciamiento" name="hddSecfinanciamiento" value="" runat="server" />
        <input type="hidden" id="hddFechaHoy" name="hddFechaHoy" value="" runat="server" />
        

    </form>
    
</body>
</html>
