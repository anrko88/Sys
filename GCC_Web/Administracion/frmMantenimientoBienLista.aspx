<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoBienLista.aspx.vb" 
    Inherits="Administracion_frmMantenimientoBienLista" %>

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
    <script src="../Util/js/jquery/jquery-1.6.2.min.js" type="text/javascript"></script>
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
    <script type='text/javascript' src="frmMantenimientoBienLista.aspx.js"> </script>

</head>
<body>
    <form id="frmGestionBienLista" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestion del Bien</div>
                    <div class="css_lbl_titulo">
                        Bien :: Listado</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscarBien(true);">
                            <img alt="" src="../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Limpiar();">
                            <img alt="" src="../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Editar();">
                            <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" /><br />
                            Editar </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                        <input type="hidden" id="hidSecFinanciamiento" value="0" />
                        <input type="hidden" id="hidCodigoSolicitudCredito" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="width: 419px">
                                    Datos de Gestion del Bien
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                 <tr>
                                    <td style="width:150px;"></td>
                                    <td style="width:200px;"></td>
                                    <td style="width:150px;"></td>
                                    <td style="width:200px;"></td>
                                    <td style="width:150px;"></td>
                                    <td style="width:200px;"></td>
                                </tr>    
                                <tr>
                                    <td class="label">Nº Contrato</td>
                                    <td class="input">
                                        <input id="txtContrato" type="text" class="css_input" name="txtContrato" />
                                    </td>
                                    <td class="label" >CU Cliente</td>
                                    <td class="input" >
                                        <input id="txtCUCliente" type="text" class="css_input" name="txtCUCliente" />
                                    </td>
                                    <td class="label">Razón Social o Nombre</td>
                                    <td class="input" colspan="3">
                                        <input id="txtRazonSocial" type="text" class="css_input" style="width: 200px" name="txtRazonSocial" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">Clasificación del Bien</td>
                                    <td class="input" colspan="3">
                                        <select id="ddlClasificacionbien" name="ddlClasificacionbien" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">Tipo del Bien</td>
                                    <td class="input" width="220px">
                                        <select id="ddlTipobien" name="ddlTipobien">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="label">Fecha de Transferencia</td>
                                    <td class="input">
                                        <input id="txtFechaTransferencia" name="txtFechaTransferencia" type="text"
                                            class="css_input" size="11" runat="server" />
                                    </td>
                                    <td class="label">Departamento</td>
                                    <td class="input">
                                        <select id="ddlDepartamento" name="ddlDepartamento" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">Estado del Bien</td>
                                    <td class="input" colspan="3">
                                         <select id="ddlEstado" name="ddlEstado" runat="server">
                                        </select>
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
    </div>
    </form>
</body>
</html>
