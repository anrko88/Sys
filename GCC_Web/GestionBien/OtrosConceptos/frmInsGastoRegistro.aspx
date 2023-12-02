<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInsGastoRegistro.aspx.vb"
    Inherits="GestionBien_OtrosConceptos_frmInsGastoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
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
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_A.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_B.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_C.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_D.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_E.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_F.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmInsGastoRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmInsDesembolsoListado" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_insDesembolso.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos" style="width:400px">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Otros Conceptos(Instrucción Gasto) :: Registro</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_botonCancelar" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <div class="dv_img_boton_separador" id="dv_SeparadorGuardar">
                        <img alt="" src="../../Util/images/ico_separador.gif" border="0" /><br />
                    </div>
                    <div id="dv_botonEnviar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_actualizarEstadoID('003');" title="Ejecutar Instrucción de Desembolso">
                            <img alt="" src="../../Util/images/ico_acc_enviarID.gif" border="0" width="35px" /><br />
                            Enviar </a>
                    </div>
                    <div class="dv_img_boton_separador" id="dv_SeparadorGuardar">
                        <img alt="" src="../../Util/images/ico_separador.gif" border="0" /><br />
                    </div>
                    <div id="dv_botonRecalcular" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_volverDocumento();" title="Volver a Documentos">
                            <img alt="" src="../../Util/images/ico_acc_volverDoc.gif" border="0" width="35px" /><br />
                            Volver </a>
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
                                    Datos de la Instrucción
                                </td>
                                <td class="botones">
                                    <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        N° Instrucción
                                    </td>
                                    <td class="input">
                                        <input id="txtNroInstruccion" type="text" class="css_input" name="txtNroInstruccion" />
                                    </td>
                                    <td class="label">
                                        Estado Instrucción
                                    </td>
                                    <td class="input">
                                        <select id="cmbEstadoInstruccion" runat="server" name="cmbEstadoInstruccion">
                                            <option value='0'>[-Seleccione-]</option>
                                            <option value='1'>EN ELABORACION</option>
                                            <option value='2'>PENDIENTE DE EJECUCION</option>
                                            <option value='3'>APROBADA</option>
                                            <option value='4'>DEVUELTA</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Total Abono S/.</td>
                                    <td class="input">
                                        <input id="txtTotalAbonoSoles" name="txtTotalAbonoSoles" type="text" class="css_input_inactivo"
                                            size="12" disabled="disabled" /></td>
                                    <td class="label">
                                        Total Abono US$</td>
                                    <td class="input">
                                        <input id="txtTotalAbonoDolares" name="txtTotalAbonoDolares" type="text" class="css_input_inactivo"
                                            size="12" disabled="disabled" /></td>
                                </tr>
                            </table>
                        </div>
                        <!-- ******************************************************************************************** -->
                        <!-- Inicia Tabs -->
                        <!-- ******************************************************************************************** -->
                        <div id="divTabs" style="border: 0px; background: none;" class="dv_tabla_contenedora">
                            <ul>
                                <li><a href="#tab-0">CONSOLIDADO</a></li>
                                <li><a href="#tab-1">ABONO</a></li>
                                <li><a href="#tab-2">DOCUMENTOS Y COMENTARIOS</a></li>
                            </ul>
                            <!-- **************** -->
                            <!-- TAB :: CONSOLIDADO   -->
                            <!-- **************** -->
                            <div id="tab-0">
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Documentos</legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" id="tb_linkAbreListadoDoc">
                                        <tr>
                                            <td style="text-align: right;">
                                                <a href="javascript:fn_abreListadoDocumentos();">
                                                    <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                        border="0" align="bottom" />Agregar / Quitar Documentos </a>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                        <tr>
                                            <td id="tbCronograma" style="text-align: center">
                                                <table id="jqGrid_lista_A">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_A">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <!-- **************** -->
                            <!-- TAB :: PAGOS / CARGOS -->
                            <!-- **************** -->
                            <div id="tab-1">
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Abonos</legend>
                                    <br />
                                    <table cellpadding="4" cellspacing="0" border="0" width="970px">
                                        <!-- PROVEEDOR -->
                                        <tr id="tr_proveedores">
                                            <td style="width: 10px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                onclick="javascript:fn_util_desplegar('grd_proveedores', 'imgMaxiProveedores', 'imgMiniProveedores');">
                                                <img id="imgMaxiProveedores" src="../../Util/images/ico_segMaximizar.gif" />
                                                <img id="imgMiniProveedores" src="../../Util/images/ico_segMinimizar.gif" style="display: none;" />
                                            </td>
                                            <td style="width: 830px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                class="css_lbl_subTitulo" onclick="javascript:fn_util_desplegar('grd_proveedores', 'imgMaxiProveedores', 'imgMiniProveedores');">
                                                Proveedores
                                            </td>
                                            <td style="width: 130px; text-align: right; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr id="grd_proveedores" style="display: none;">
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td colspan="2">
                                                <table id="jqGrid_lista_B">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_B" style="display: none;">
                                                </div>
                                                <br />
                                            </td>
                                        </tr>
                                        <!-- SUNAT -->
                                        <tr id="tr_sunat">
                                            <td style="width: 10px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                onclick="javascript:fn_util_desplegar('grd_sunat','imgMaxiProveedores','imgMiniProveedores');">
                                                <img id="imgMaxiSunat" src="../../Util/images/ico_segMaximizar.gif" />
                                                <img id="imgMiniSunat" src="../../Util/images/ico_segMinimizar.gif" style="display: none;" />
                                            </td>
                                            <td colspan="2" class="css_lbl_subTitulo" style="cursor: pointer; background-color: #EFEFEF;
                                                border-bottom: 3px solid #ffffff;" onclick="javascript:fn_util_desplegar('grd_sunat','imgMaxiSunat','imgMiniSunat');">
                                                SUNAT
                                            </td>
                                        </tr>
                                        <tr id="grd_sunat" style="display: none;">
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td colspan="2">
                                                <table id="jqGrid_lista_C">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_C" style="display: none;">
                                                </div>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>                              
                            </div>
                            <!-- ******************************* -->
                            <!-- TAB :: DOCUMENTOS -->
                            <!-- ******************************* -->
                            <div id="tab-2" style="height: 100%;">
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="min-width: 100%;">
                                    <tr>
                                        <td colspan="4">
                                            <br />
                                            <input type="hidden" id="hddCodigoDocumento" name="hddCodigoDocumento" value="" runat="server" />
                                            <input type="button" name="btnListaDoc" id="btnListaDoc" onclick="javascript:fn_cargaGrillaDocumento();"
                                                style="display: none;" />
                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 880px;" id="tb_botonesDocumentos">
                                                <tr>
                                                    <td align="left">
                                                        <div id="dvBotonEliminaComentario" class="dv_img_boton_mini" style="border: 0px">
                                                            <a href="javascript:fn_eliminarDocumentoComentario();">
                                                                <img alt="" src="../../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                                    border="0" align="bottom" />Eliminar </a>
                                                        </div>
                                                        <div id="dvBotonEditaComentario" class="dv_img_boton_mini" style="border: 0px">
                                                            <a href="javascript:fn_editarDocumentoComentario();">
                                                                <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                                    border="0" align="bottom" />Editar </a>
                                                        </div>
                                                        <div id="dvBotonagregaComentario" class="dv_img_boton_mini" style="border: 0px">
                                                            <a href="javascript:fn_abreNuevoDocumentoComentario();">
                                                                <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                                    border="0" align="bottom" />Agregar </a>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--Inicia Carga Grilla -->
                                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 850px">
                                                <tr>
                                                    <td>
                                                        <table id="jqGrid_lista_D">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--Finaliza Carga Grilla -->
                                        </td>
                                    </tr>
                                </table>
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
