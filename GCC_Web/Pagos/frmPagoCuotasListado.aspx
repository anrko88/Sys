<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPagoCuotasListado.aspx.vb" Inherits="Pagos_frmPagoCuotasListado" %>

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
    <script type='text/javascript' src="../Util/js/jquery/jquery-1.7.2.min.js"> </script>
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
    <script type="text/javascript" src="../Util/js/jquery/jquery.dateFormat-1.0.js"></script>
    <script type="text/javascript" src="../Util/js/jquery/jshashtable.js"></script>    
    <script type="text/javascript" src="../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->
    <script type='text/javascript' src="frmPagoCuotasListado.aspx.js"> </script>

</head>
<body>
    <form id="frmPagoCuotasListado" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_Pagos.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Pagos</div>
                    <div class="css_lbl_titulo">
                        Pago de Cuotas ::  <label id="lblOperacion" runat="server"> Listado </label></div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscarPagoCuotas(true);">
                            <img alt="" src="../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_limpiarForm();">
                            <img src="../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreEditar()">
                            <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" /><br />
                            Editar </a>
                    </div>      
                    <div id="Div1" class="dv_img_boton">
                        <a href="javascript:fn_agregar();">
                            <img alt="" src="../Util/images/ico_acc_agregar.gif" border="0" /><br />
                            Agregar </a>
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
                                    <img src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        
                        <input type="hidden" name="hddError" id="hddError" value="" runat="server" />
                        <input type="hidden" name="hddCodSolicitudCredito" id="hddCodSolicitudCredito" value="" runat="server" />
                        <input type="hidden" name="hddNumSecRecuperacion" id="hddNumSecRecuperacion" value="" runat="server" />
                        <input type="hidden" name="hddTipoTransaccion" id="hddTipoTransaccion" value="" runat="server" />
                        
                        <input type="button" name="btnBuscar" id="btnBuscar" onclick="javascript:fn_buscarPagoCuotas(true);" style="display: none;" />
                        
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input" />
                                    </td>
                                    <td class="label">
                                        CU. Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtCuCliente" type="text" class="css_input" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" type="text" class="css_input" size="50" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Nº Autorización
                                    </td>
                                    <td class="input">
                                        <input id="txtNroAutorizacion" type="text" class="css_input" />
                                    </td>
                                    <td class="label">
                                        Fecha Pago Inicio                                     </td>
                                    <td class="input">
                                        <input id="txtFechaPagoIni" type="text" class="css_input" size="11" />
                                    </td>
                                    <td class="label">
                                        Fecha Pago Fin
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaPagoFin" type="text" class="css_input" size="11" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo Contrato
                                    </td>
                                    <td class="input">
                                        <select id="cmbTipoContrato" name="cmbTipoContrato" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">
                                        Estado
                                    </td>
                                    <td class="input">
                                        <select id="cmbEstado" name="cmbEstado"  runat="server">
                                        </select>
                                    </td>
                                     <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <select id="cmbMoneda" name="cmbMoneda"  runat="server">
                                        </select>
                                    </td>
                                </tr>
                            </table>                                                      
                        </div>
                            
                        <br />
                        <!-- Inicio Grilla Listado de Pagos -->
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
        <!-- Fin Cuerpo -->
        
    </div>
    </form>
</body>
</html>
