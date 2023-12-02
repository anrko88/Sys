<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmENegocioListado.aspx.vb" Inherits="Administracion_frmENegocioListado" %>

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

    <script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"></script>
    <script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>
    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"></script>
    <script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"></script>
    <script type='text/javascript' src="../Util/js/jquery/jquery.ui.global.js"> </script>
    <script type='text/javascript' src="../Util/js/jquery/jquery.validText.js"> </script>
    <script type='text/javascript' src="../Util/js/jquery/jquery.validNumber.js"> </script>
    <script type='text/javascript' src="../Util/js/jquery/jquery.maxlength.js"> </script>
    <script type="text/javascript" src="../Util/js/jquery/json2.js"></script>
    <script type="text/javascript" src="../Util/js/js_global.js"></script>
    <script type='text/javascript' src="../Util/js/js_util.modal.js"></script>
    <script type='text/javascript' src="../Util/js/js_util.funcion.js"></script>
    <script type='text/javascript' src="../Util/js/js_util.date.js"></script>
    <script type='text/javascript' src="../Util/js/js_util.ajax.js"></script>
    
    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->
    <script type='text/javascript' src="frmENegocioListado.aspx.js"> </script>

</head>
<body>
    <form id="frmProveedorListado" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_siniestro.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Mantenimiento</div>
                    <div class="css_lbl_titulo">
                        Mantenimiento :: Ejecutivos de Negocio</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscarEjecutivo(true);">
                            <img alt="" src="../Util/images/ico_acc_buscar.gif" border="0" title="Buscar" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_LimpiarCampos();">
                            <img alt="" src="../Util/images/ico_acc_limpiar.gif" border="0" title="Limpiar" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_GBEliminar" class="dv_img_boton">
                        <a href="javascript:fn_eliminar()">
                            <img alt="" src="../Util/images/ico_acc_eliminar.gif" border="0" /><br />
                            Eliminar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreEditar()">
                            <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" title="Editar" /><br />
                            Editar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreNuevo();">
                            <img alt="" src="../Util/images/ico_acc_agregar.gif" border="0" title="Agregar" /><br />
                            Agregar </a>
                    </div>
                    
                    <div id="dv_img_boton" class="dv_img_boton" style="width:100px;">
                        <a href="javascript:fn_abreELeasing();">
                            <img alt="" src="../Util/images/ico_mdl_cesion.gif" border="0" title="Agregar" width="35" /><br />
                            Ejec. Leasing </a>
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
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos de búsqueda
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        
                        <input type="hidden" name="hddCodEjecutivo" id="hddCodEjecutivo" value=""/>
                        <input type="button" name="btnBuscar" id="btnBuscar" onclick="javascript:fn_buscarEjecutivo(true);" style="display: none;" />
                                                
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Tipo Ejecutivo
                                    </td>
                                    <td class="input">
                                        <select id="cmbTipo" name="cmbTipo" runat="server">
                                            <option value="0">- Seleccionar -</option>
                                            <option value="1">Ejecutivo Negocio</option>
                                            <option value="2">Ejecutivo Leasing</option>                                            
                                        </select>
                                    </td>
                                    <td class="label">
                                        Código Ejecutivo
                                    </td>
                                    <td class="input">
                                        <input id="txtCodEjecutivo" type="text" class="css_input" size="12" />
                                    </td>
                                    <td class="label" style="width: 200px">
                                        Nombre Ejecutivo
                                    </td>
                                    <td class="input">
                                        <input id="txtNomEjecutivo" type="text" class="css_input" runat="server" size="75" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div class="dv_tabla_contenedora">
                            <table id="jqGrid_lista_A">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_A">
                            </div>
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
