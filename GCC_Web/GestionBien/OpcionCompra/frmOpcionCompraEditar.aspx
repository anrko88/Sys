<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOpcionCompraEditar.aspx.vb"
    Inherits="GestionBien_OpcionCompra_frmOpcionCompraEditar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Opcion de Compra </title>
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

    <script type='text/javascript' src="frmOpcionCompraEditar.aspx.js"> </script>

</head>
<body>
    <form id="frmEditarOpcionCompra" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <img src="../../Util/images/ico_mdl_multa.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo" id="dvTitulo">
                        Opción de Compra: Editar</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="Div3" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_guardar" class="dv_img_boton">
                        <div id="dv_img_boton" class="dv_img_boton">
                            <a href="javascript:fn_guardar();">
                                <img src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                                Guardar </a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            padding-right: 0px;">
            <tr>
                <td class="lineas">
                </td>
            </tr>
            <tr>
                <td class="cuerpo">
                    <div id="dv_datos" class="dv_tabla_contenedora">
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                            <tr>
                                <td class="label" style="width:230px">
                                    Moneda
                                </td>
                                <td class="input">
                                    <input id="txtMoneda" name="txtTipoDocumento" runat="server" type="text" class="css_input_inactivo"
                                            size="12" readonly="true" /></td>
                            </tr>
                            <tr>
                                <td class="label" style="width:230px">
                                    Opción de Compra %</td>
                                <td class="input">
                                        <input id="txtPorcentajeOC" name="txtPorcentajeOC" runat="server" type="text" class="css_input_inactivo"
                                            size="12" readonly="true" style="text-align:right" /></td>
                            </tr>
                            <tr>
                                <td class="label" style="width:230px">
                                    Importe Opción de Compra</td>
                                <td class="input">
                                        <input id="txtImporteOC" name="txtImporteOC" runat="server" type="text" class="css_input_inactivo"
                                            size="12" readonly="true" style="text-align:right" /></td>
                            </tr>
                            <tr>
                                <td class="label" style="width:230px">
                                    Comisión Opción de Compra
                                </td>
                                <td class="input">
	                                <input type="hidden" name="hddComisionOC" id="hddComisionOC" value="0" runat="server" />
	
									<input id="rdbComisionOCPorc" name="rdbComisionOC" type="radio" runat="server" onclick="javacript:fn_validaRadio('1','hddComisionOC','txtComisionOCMonto',1);"/>						                            
                                    <input id="txtComisionOC" name="txtComisionOC" runat="server" type="text" class="css_input" size="12" style="text-align:right; margin:1px;" /> %                                         
                                    <br/>                                            
                                    <input id="rdbComisionOCMonto" name="rdbComisionOC" type="radio" runat="server" onclick="javacript:fn_validaRadio('2','hddComisionOC','txtComisionOC',1);"/>						                            
                                    <input id="txtComisionOCMonto" name="txtComisionOCMonto" runat="server" type="text" class="css_input" size="12" style="text-align:right; margin:1px;" /> (Monto)
                                        
								</td>
                            </tr>
                            <tr>
                                <td class="label" style="width:230px">
                                    Comisión de Gastos de Transferencia
                                </td>
                                <td class="input">                                
									<input type="hidden" name="hddGastosTransCGT" id="hddGastosTransCGT" value="0" runat="server" />

									<input id="rdbGastosTransCGTPorc" name="rdbGastosTransCGT" type="radio" runat="server" onclick="javacript:fn_validaRadio('1','hddGastosTransCGT','txtPorcentajeCGTMonto',2);"/>						                            
									<input id="txtPorcentajeCGT" name="txtPorcentajeCGT" runat="server" type="text" class="css_input" size="12" style="text-align:right; margin:1px;" /> %                                     
									<br/>      
									<input id="rdbGastosTransCGTMonto" name="rdbGastosTransCGT" type="radio" runat="server" onclick="javacript:fn_validaRadio('2','hddGastosTransCGT','txtPorcentajeCGT',2);"/>						                                                                       
									<input id="txtPorcentajeCGTMonto" name="txtPorcentajeCGTMonto" runat="server" type="text" class="css_input" size="12" style="text-align:right; margin:1px;" /> (Monto)
                                     
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" name="hidCodOpcionCompra" id="hidCodOpcionCompra" value="0" runat="server" />
    <input type="hidden" name="hidNumeroContrato" id="hidNumeroContrato" value="0" runat="server" />
    <input type="hidden" name="hidOpcion" id="hidOpcion" value="0" runat="server" />
    </form>
</body>
</html>
