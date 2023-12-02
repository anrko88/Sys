<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGastoListado.aspx.vb"
    Inherits="GestionBien_OtrosConceptos_frmGastoListado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SGL :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css"
        media="all" />
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

    <script type='text/javascript' src="frmGastoListado.aspx.js"> </script>

</head>
<body>
    <form id="frmGastoListado" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_version.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Otros Conceptos (Gastos) :: Listado</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscar(true);">
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
                        <a href="javascript:fn_abreEditar('')">
                            <img alt="" src="../../Util/images/ico_acc_editar.gif" border="0" /><br />
                            Editar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreNuevo();">
                            <img alt="" src="../../Util/images/ico_acc_agregar.gif" border="0" /><br />
                            Agregar </a>
                    </div>
				    <div id="dv_botonWIO" class="dv_img_boton" style="width:80px;">
					    <a href="javascript:fn_ValidaGenerarWIO();" title="Genera WIO">
						    <img alt="" src="../../Util/images/ico_acc_ejecutarWIO.gif" border="0" width="35px"/><br/>
						    Generar WIO
					    </a>
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
                                        Tipo Documento
                                    </td>
                                    <td class="input">
                                        <select id="cmbTipoDocumento" runat="server" name="cmbTipoDocumento" runat="server">
                                             <option>[-Seleccione-]</option>
                                            <option>RUC</option>
                                            <option>DNI</option>
                                            <option>OTRO DOCUMENTO</option>
                                        </select></td>
                                    <td class="label">
                                        N° Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocumento" type="text" class="css_input" name="txtNroDocumento" /></td>
                                    <td class="label">
                                        Razón Social / Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" type="text" class="css_input" size="50" name="txtRazonSocial" /></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo Comprobante
                                    </td>
                                    <td class="input">
                                        <select id="cmbTipoComprobante" runat="server" name="cmbTipoComprobante">
                                            <option>[-Seleccione-]</option>
                                            <option>FACTURA</option>
                                            <option>BOLETA</option>
                                            <option>RECIBO POR HONORARIOS</option>
                                            <option>IRPES</option>
                                            <option>OTROS</option>                                            
                                        </select></td>
                                    <td class="label">
                                        N° Comprobante
                                    </td>
                                    <td class="input">
                                        <input id="txtNroComprobante" type="text" class="css_input" name="txtNroComprobante" /></td>
                                    <td class="label">
                                        Fecha Registro Desde</td>
                                    <td class="input">
                                        <input id="txtFechaIni" name="txtFechaIni" type="text" class="css_input" size="11"
                                            runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        &nbsp;Estado Pago
                                    </td>
                                    <td class="input">
                                        <select id="cmbEstadoPago" runat="server" name="cmbEstadoPago">
                                            <option>[-Seleccione-]</option>
                                            <option>PENDIENTE</option>
                                            <option>POR PAGAR</option>
                                            <option>PAGADO</option>
                                            <option>ANULADO</option>
                                        </select></td>
                                    <td class="label">
                                        Estado Cobro
                                    </td>
                                    <td class="input">
                                        <select id="cmbEstadoCobro" runat="server" name="cmbEstadoCobro">
                                            <option>[-Seleccione-]</option>
                                            <option>COBRADO TOTAL</option>
                                            <option>COBRADO PARCIAL</option>
                                        </select></td>
                                    <td class="label">
                                        Fecha Registro Hasta</td>
                                    <td class="input">
                                        <input id="txtFechaFin" name="txtFechaFin" type="text" class="css_input" size="11"
                                            runat="server" /></td>
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
