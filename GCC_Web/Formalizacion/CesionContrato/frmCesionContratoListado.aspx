<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCesionContratoListado.aspx.vb" Inherits="Formalizacion_CesionContrato_frmCesionContratoListado" %>

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
    <script type='text/javascript' src="frmCesionContratoListado.aspx.js"> </script>

</head>
<body>

    <form id="frmSiniestroListado" runat="server">
    
    <input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
    <input type="hidden" name="hddCodContratoTemp" id="hddCodContratoTemp" value="" runat="server" />
        
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
                    <div class="css_lbl_titulo">Cesión de Contrato :: Listado</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscarCesion(true);">
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
                    <div id="dv_img_boton" class="dv_img_boton" style="width:65px;">
                        <a href="javascript:fn_abreEditar('')">
                            <img alt="" src="../../Util/images/ico_acc_editar.gif" border="0" /><br />
                            Cesionarios </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width:65px;">
                        <a href="#">
                            <img alt="" src="../../Util/images/ico_acc_ejecutarWIO.gif" border="0" /><br />
                            Liquidación </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width:90px;">
                        <a href="javascript:fn_abreCesionarios();">
                            <img alt="" src="../../Util/images/ico_acc_agregar.gif" border="0" /><br />
                            Cesionarios </a>
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
                                    <td class="label">
                                        Nro. Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input" size="10" />
                                    </td>                                    
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtCUCliente" type="text" class="css_input" size="13" />
                                    </td> 
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" type="text" class="css_input" size="54" />
                                    </td>                                   
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo Documento
                                    </td>
                                    <td class="input">
                                        <select id="cmdTipoDoc" name="cmdTipoDoc" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">
                                        Nº Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocumento" type="text" class="css_input" size="13" />
                                    </td>
                                    <td class="label">
                                        Clasificación Bien
                                    </td>
                                    <td class="input">
                                        <select id="cmdClasificacion" name="cmdClasificacion" runat="server">
                                        </select>
                                    </td>
                                </tr>                                
                                <tr>
									<td class="label">
                                        Estado de Contrato
                                    </td>
                                    <td class="input">
                                        <select id="cmbEstadoContrato" name="cmbEstadoContrato" runat="server">	
											<option value="0"> - Seleccionar -</option>       
                                        </select>
                                    </td>                                    
                                    <td class="label" style="width:150px">
                                        Cesión de Posición
                                    </td>
                                    <td colspan="3" class="input">
                                        <input type="hidden" name="hddCesionPosicion" id="hddCesionPosicion" value="0" runat="server" />
				                        <input id="rdbCesionPosicionSI" name="rdbCesionPosicion" type="radio" runat="server" onclick="javascript:fn_seteaValorRadio('1')"/>SI  
                                        <input id="rdbCesionPosicionNO" name="rdbCesionPosicion" type="radio" runat="server" onclick="javascript:fn_seteaValorRadio('0')"/>NO
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
