<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaImpuestoMultaInmuebleListado.aspx.vb" Inherits="Consultas_ImpuestoMultasInmueble_frmConsultaImpuestoMultaInmuebleListado" %>

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
    <script type='text/javascript' src="frmConsultaImpuestoMultaInmuebleListado.aspx.js"> </script>

</head>
<body>

    <form id="frmImpuestoMultaInmuebleListado" runat="server">
    
		<input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
		<input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />				
		<input type="hidden" name="hddCodImpuesto" id="hddCodImpuesto" value="" runat="server" />
		
		<input type="button" name="btnBuscarImpuestos" id="btnBuscarImpuestos" onclick="javascript:fn_realizaBusquedaImpuestoMuni();" style="display: none;" />
		    
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_impuesto.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">Consultas</div>
                    <div class="css_lbl_titulo">Impuestos y Multas Inmuebles :: Listado</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones" style="height:60px;vertical-align:top;">
                
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscarImpuestoMuni(true);">
                            <img alt="" src="../../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_limpiar();">
                            <img src="../../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreEditar()">
                            <img alt="" src="../../Util/images/ico_acc_ver.gif" border="0" /><br />
                            Ver </a>
                    </div>
                    
                     <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Reporte();">
                            <img alt="" src="../../Util/images/ico_acc_importar.gif" border="0" /><br />
                            Exportar </a>
                    </div>
                    
                </td>
            </tr>
        </table>
        
        
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                        <asp:Button ID="btnGenerar" runat="server" Style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                    
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos de Búsqueda
                                </td>
                                <td class="botones">
                                    <img src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                                               
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">Departamento</td>
                                    <td class="input">
                                        <select id="cmbDepartamento" name="cmbDepartamento" onchange="javascript:fn_cargaComboProvincia(this.value);" runat="server">
                                           <option value="0">[-Seleccionar-]</option>                                                  
                                        </select>
                                    </td>      
                                    
                                    <td class="label">
                                        Provincia 
                                    </td>
                                    <td class="input">
                                         <select id="cmbProvincia" name="cmbProvincia" onchange="javascript:fn_cargaComboDistrito(cmbDepartamento.value,this.value);" runat="server">
											<option value="0">[-Seleccionar-]</option>														
                                         </select>
                                    </td>
                                    <td class="label">Distrito</td>
                                    <td class="input">
                                         <select id="cmbDistrito" name="cmbDistrito" runat="server">
											<option value="0">[-Seleccionar-]</option>                                                  
                                         </select>
                                    </td>
                                </tr>      
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input" size="10" runat="server" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input" colspan="3">
                                        <input id="txtRazonSocial" type="text" class="css_input" size="60" runat="server"/>
                                    </td>                                    
                                </tr>                                          
                                <tr>
                                    <td class="label">
                                       Tipo de Documento
                                    </td>
                                    <td class="input">
                                        <select id="cmdTipoDoc" name="cmdTipoDoc" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">
                                        Nº Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocumento" type="text" class="css_input" size="13" runat="server"/>
                                    </td>       
                                    <td class="label">
                                        Periodo
                                    </td>
                                    <td class="input">
                                        <input id="txtPeriodo" type="text" class="css_input" size="5" runat="server"/>
                                    </td>                                                                                                     
                                </tr>                                          
                                <tr>
									<td class="label">
                                        Estado del Pago
                                    </td>
                                    <td class="input">
                                        <select id="cmdEstadoPago" name="cmdEstadoPago" runat="server">
                                        </select>
                                    </td>
									<td class="label">
                                       Estado del Cobro
                                    </td>
                                    <td class="input">
                                        <select id="cmdEstadoCobro" name="cmdEstadoCobro" runat="server">
                                        </select>
                                    </td>                                    
                                    <td class="label">
                                        Nº Lote
                                    </td>
                                    <td class="input">
                                        <input id="txtLote" type="text" class="css_input" size="11" runat="server"/>
                                    </td>
                                </tr>                                          
                            </table>                                                      
                        </div>
                            
                        <br />
                        <!-- Inicio Grilla Listado -->
                        <div class="dv_tabla_contenedoraSola">
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
        
    </div>
    <!-- Fin Cuerpo -->
     
    </form>
    
</body>
</html>
