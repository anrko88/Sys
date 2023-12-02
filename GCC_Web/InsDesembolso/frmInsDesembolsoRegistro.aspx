<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInsDesembolsoRegistro.aspx.vb"
    Inherits="InsDesembolso_frmInsDesembolsoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css"
        media="all" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_fuente.css" />
    <!-- JavaScript -->

    <script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"> </script>

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
    <link type="text/css" rel="stylesheet" href="../Util/css/jqgrid/css_grilla_A.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jqgrid/css_grilla_B.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jqgrid/css_grilla_C.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jqgrid/css_grilla_D.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jqgrid/css_grilla_E.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jqgrid/css_grilla_F.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmInsDesembolsoRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmInsDesembolsoRegistro" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_insDesembolso.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos" style="width: 237px">
                    <div class="css_lbl_subTitulo">
                        Desembolso</div>
                    <div class="css_lbl_titulo">
                        Instrucción Desembolso :: Editar</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_botonCancelar" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <div class="dv_img_boton_separador" id="dv_SeparadorGuardar">
                        <img alt="" src="../Util/images/ico_separador.gif" border="0" /><br />
                    </div>
                    <div id="dv_botonRecalcular" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_recalcular();" title="Recalcular">
                            <img alt="" src="../Util/images/ico_acc_refrescar.gif" border="0" width="35px" /><br />
                            Recalcular </a>
                    </div>
                    <div id="dv_botonWIO" class="dv_img_boton" style="width: 80px; display: none;">
                        <a href="javascript:fn_ValidaGenerarWIO();" title="Genera WIO">
                            <img alt="" src="../Util/images/ico_acc_ejecutarWIO.gif" border="0" width="35px" /><br />
                            Generar WIO </a>
                    </div>
                    <div id="dv_botonEjecutar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_ejecutarID();" title="Ejecutar Instrucción de Desembolso">
                            <img alt="" src="../Util/images/ico_acc_ejecutarID.gif" border="0" width="35px" /><br />
                            Ejecutar </a>
                    </div>
                    <!-- Inicio IBK - AAE - Agrego Botones -->
                    <div id="dv_botonPagoAdministrativo" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_Administrativo();" title="Pago Administrativo">
                            <img alt="" src="../Util/images/gcc_pago_administrativo.png" border="0" width="35px" /><br />
                            Administrativo </a>
                    </div>
                    <div id="dv_botonReintentar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_ReintentarPago();" title="Reintentar">
                            <img alt="" src="../Util/images/gcc_pago_reintentar.png" border="0" width="35px"
                                style="height: 35px; width: 35px" /><br />
                            Reintentar </a>
                    </div>
                    <div id="dv_botonAnular" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_Anular();" title="Reintentar">
                            <img alt="" src="../Util/images/ico_acc_devolverID.gif" border="0" width="35px" /><br />
                            Anular </a>
                    </div>
                    <!-- Fin IBK -->
                    <div id="dv_botonDevolver" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_actualizarEstadoID('004');" title="Devolver Instrucción de Desembolso">
                            <img alt="" src="../Util/images/ico_acc_devolverID.gif" border="0" width="35px" /><br />
                            Devolver </a>
                    </div>
                    <div id="dv_botonEnviar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_actualizarEstadoID('003');" title="Ejecutar Instrucción de Desembolso">
                            <img alt="" src="../Util/images/ico_acc_enviarID.gif" border="0" width="35px" /><br />
                            Enviar </a>
                    </div>
                    <div class="dv_img_boton_separador" id="dv_SeparadorGuardar">
                        <img alt="" src="../Util/images/ico_separador.gif" border="0" /><br />
                    </div>
                    <div id="dv_botonRecalcular" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_volverDocumento();" title="Volver a Documentos">
                            <img alt="" src="../Util/images/ico_acc_volverDoc.gif" border="0" width="35px" /><br />
                            Volver </a>
                    </div>
                    <div id="dv_tempWio" class="dv_img_boton">
                        <a href="javascript:fn_actualizarEstadoID('002');" title="Ejecutar Instrucción de Desembolso">
                            . </a>
                    </div>
                </td>
            </tr>
        </table>
        <input type="hidden" name="hddError" id="hddError" value="" runat="server" />
        <input type="hidden" name="hddActivacion" id="hddActivacion" value="" runat="server" />
        <input type="hidden" name="hddCodigoInsDesembolso" id="hddCodigoInsDesembolso" value=""
            runat="server" />
        <input type="hidden" name="hddCodigoAgrupacion" id="hddCodigoAgrupacion" value=""
            runat="server" />
        <input type="hidden" name="hddCodMonedaContrato" id="hddCodMonedaContrato" value=""
            runat="server" />
        <input type="hidden" name="hddCodCotizacion" id="hddCodCotizacion" value="" runat="server" />
        <input type="hidden" name="hddCodEstadoInstruccion" id="hddCodEstadoInstruccion"
            value="" runat="server" />
        <input type="hidden" name="hddModoVer" id="hddModoVer" value="" runat="server" />
        <input type="hidden" name="hddCodAgrupacion" id="hddCodAgrupacion" value="" runat="server" />
        <input type="hidden" name="hddCodProveedor" id="hddCodProveedor" value="" runat="server" />
        <input type="hidden" name="hddCodMonedaAgrupacion" id="hddCodMonedaAgrupacion" value=""
            runat="server" />
        <input type="hidden" name="hddCodAgrupacionModal" id="hddCodAgrupacionModal" value=""
            runat="server" />
        <!-- Inicio IBK - AAE - Agrego el estado de envío a LPC-->
        <input type="hidden" name="hddFlagEnvioLPC" id="hddFlagEnvioLPC" value="" runat="server" />
        <input type="hidden" id="hddMontoTemporal" name="hddMontoTemporal" value="" runat="server" />
        <input type="hidden" id="hddCodigoSubtipoContrato" name="hddCodigoSubtipoContrato"
            value="" runat="server" />
        <input type="hidden" id="hddTipoPersona" name="hddTipoPersona" value="" runat="server" />
        <!-- Fin IBK-->
        <input type="hidden" id="hddCorrelativo" name="hddCorrelativo" value="" runat="server" />
        <input type="button" name="btnBuscar" id="btnBuscar" onclick="javascript:fn_buscarInsDesembolso(true);"
            style="display: none;" />
        <input type="button" name="btnListaAgrupacion" id="btnListaAgrupacion" onclick="javascript:fn_recargaID();"
            style="display: none;" />
        <input type="button" name="btnListaAdelantos" id="btnListaAdelantos" onclick="javascript:fn_listaCargoAbono('C'); fn_cargaDatosID(); fn_listaCargoAbono('B');"
            style="display: none;" />
        <input type="button" name="btnListaCargos" id="btnListaCargos" onclick="javascript:fn_actualizaListaCargo('F'); fn_cargaDatosID();"
            style="display: none;" />
        <input type="button" name="btnActualizaGrupos" id="btnActualizaGrupos" onclick="javascript:fn_ActualizarGrupos();"
            style="display: none;" />
        <asp:HiddenField ID="hidDesembolso" runat="server" />
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <fieldset>
                                <legend class="css_lbl_subTitulo">Datos del Contrato</legend>
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td class="label">
                                            Nº Contrato
                                        </td>
                                        <td class="input">
                                            <input id="txtNroContrato" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            CU. Cliente
                                        </td>
                                        <td class="input">
                                            <input id="txtCuCliente" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Razón Social o Nombre
                                        </td>
                                        <td class="input" colspan="3">
                                            <input id="txtRazonSocial" type="text" class="css_input" size="50" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Nº Instrucción
                                        </td>
                                        <td class="input">
                                            <input id="txtNroInstruccion" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Tipo Contrato
                                        </td>
                                        <td class="input">
                                            <input id="txtTipoContrato" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Estado Instrucción
                                        </td>
                                        <td class="input" colspan="3">
                                            <input id="txtEstado" type="text" class="css_input" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Valor Venta
                                        </td>
                                        <td class="input">
                                            <input id="txtValorVenta" type="text" size="12" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Igv
                                        </td>
                                        <td class="input">
                                            <input id="txtIgv" type="text" size="12" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Precio Venta
                                        </td>
                                        <td class="input">
                                            <input id="txtPrecioventa" type="text" size="12" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Activacion Leasing
                                        </td>
                                        <td>
                                            <input id="chkActivacion" type="checkbox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Moneda
                                        </td>
                                        <td class="input">
                                            <input id="txtMoneda" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            TC. del Dia
                                        </td>
                                        <td class="input">
                                            <input id="txtTcDia" type="text" size="10" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            TC. Ticket
                                        </td>
                                        <td class="input" colspan="3">
                                            <input id="txtNroTicket" type="text" size="6" class="css_input" runat="server" />
                                            <input id="txtTcTicket" type="text" size="10" class="css_input" runat="server" />
                                            <img alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                                id="imgNumeroTipo" runat="server" width="17" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend class="css_lbl_subTitulo">Totales</legend>
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td class="label">
                                            Total Desembolsado
                                        </td>
                                        <td class="input">
                                            <input id="txtTotalDesembolsado" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Total Pagos
                                        </td>
                                        <td class="input">
                                            <input id="txtTotalPagos" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Total Cargos
                                        </td>
                                        <td class="input">
                                            <input id="txtTotalCargos" type="text" class="css_input" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <!-- ******************************************************************************************** -->
                        <!-- Inicia Tabs -->
                        <!-- ******************************************************************************************** -->
                        <div id="divTabs" style="border: 0px; background: none;" class="dv_tabla_contenedora">
                            <ul>
                                <li><a href="#tab-0">CONSOLIDADO</a></li>
                                <li><a href="#tab-1">ABONOS / CARGO</a></li>
                                <li><a href="#tab-2">DOCUMENTOS Y COMENTARIOS</a></li>
                                <li id="tabAct"><a href="#tab-3">ACTIVACION</a></li>
                                <li id="tabCro"><a href="#tab-4">CRONOGRAMA</a></li>
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
                                                    <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
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
                                                onclick="javascript:fn_util_desplegar('grd_proveedores', 'imgMaxiProveedores', 'imgMiniProveedores');fn_util_desplegar('tot_proveedores', 'imgMaxiProveedores', 'imgMiniProveedores');">
                                                <img id="imgMaxiProveedores" src="../Util/images/ico_segMaximizar.gif" />
                                                <img id="imgMiniProveedores" src="../Util/images/ico_segMinimizar.gif" style="display: none;" />
                                            </td>
                                            <td style="width: 830px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                class="css_lbl_subTitulo" onclick="javascript:fn_util_desplegar('grd_proveedores', 'imgMaxiProveedores', 'imgMiniProveedores');fn_util_desplegar('tot_proveedores', 'imgMaxiProveedores', 'imgMiniProveedores');">
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
                                        <!-- Inicio IBK - AAE - Agrego sumarizacion-->
                                        <tr id="tot_proveedores" style="display: none;">
                                            <td colspan="3">
                                                <table cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="label">
                                                            Total Proveedores Moneda Contrato
                                                        </td>
                                                        <td class="input">
                                                            <input id="txtTotProv" type="text" class="css_input" runat="server" align="right" />
                                                        </td>
                                                        <td />
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            Total Descuentos Moneda Contrato
                                                        </td>
                                                        <td class="input">
                                                            <input id="txtTotDtoSUNAT" type="text" class="css_input" runat="server" />
                                                        </td>
                                                        <td />
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            Total Adelantos Moneda Contrato
                                                        </td>
                                                        <td class="input">
                                                            <input id="txtTotAdelantosProv" type="text" class="css_input" runat="server" />
                                                        </td>
                                                        <td />
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <!-- Fin IBK - AAE - Agrego sumarizacion-->
                                        <!-- CLIENTE -->
                                        <tr id="tr_clientes">
                                            <td style="width: 10px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                onclick="javascript:fn_util_desplegar('grd_clientes','imgMaxiCliente','imgMiniCliente');fn_util_desplegar('tot_clientes','imgMaxiCliente','imgMiniCliente');">
                                                <img id="imgMaxiCliente" src="../Util/images/ico_segMaximizar.gif" />
                                                <img id="imgMiniCliente" src="../Util/images/ico_segMinimizar.gif" style="display: none;" />
                                            </td>
                                            <td colspan="2" class="css_lbl_subTitulo" style="cursor: pointer; background-color: #EFEFEF;
                                                border-bottom: 3px solid #ffffff;" onclick="javascript:fn_util_desplegar('grd_clientes','imgMaxiCliente','imgMiniCliente');fn_util_desplegar('tot_clientes','imgMaxiCliente','imgMiniCliente');">
                                                Adelantos al Cliente
                                            </td>
                                        </tr>
                                        <tr id="grd_clientes" style="display: none;">
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
                                        <!-- Inicio IBK - AAE - Agrego sumarizacion-->
                                        <tr id="tot_clientes" style="display: none;">
                                            <td colspan="3">
                                                <table cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="label">
                                                            Total Adelantos Moneda Contrato
                                                        </td>
                                                        <td class="input">
                                                            <input id="txtTotAdelantos" type="text" class="css_input" runat="server" />
                                                        </td>
                                                        <td />
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <!-- Fin IBK - AAE - Agrego sumarizacion-->
                                        <!-- SUNAT -->
                                        <tr id="tr_sunat">
                                            <td style="width: 10px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                onclick="javascript:fn_util_desplegar('grd_sunat','imgMaxiProveedores','imgMiniProveedores');fn_util_desplegar('tot_sunat','imgMaxiProveedores','imgMiniProveedores');">
                                                <img id="imgMaxiSunat" src="../Util/images/ico_segMaximizar.gif" />
                                                <img id="imgMiniSunat" src="../Util/images/ico_segMinimizar.gif" style="display: none;" />
                                            </td>
                                            <td colspan="2" class="css_lbl_subTitulo" style="cursor: pointer; background-color: #EFEFEF;
                                                border-bottom: 3px solid #ffffff;" onclick="javascript:fn_util_desplegar('grd_sunat','imgMaxiSunat','imgMiniSunat');fn_util_desplegar('tot_sunat','imgMaxiProveedores','imgMiniProveedores');">
                                                SUNAT
                                            </td>
                                        </tr>
                                        <tr id="grd_sunat" style="display: none;">
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td colspan="2">
                                                <table id="jqGrid_lista_D">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_D" style="display: none;">
                                                </div>
                                                <br />
                                            </td>
                                        </tr>
                                        <!-- Inicio IBK - AAE - Agrego sumarizacion-->
                                        <tr id="tot_sunat" style="display: none;">
                                            <td colspan="3">
                                                <table cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="label">
                                                            Total DUAS Moneda Contrato
                                                        </td>
                                                        <td class="input">
                                                            <input id="txtTotDUAS" type="text" class="css_input" runat="server" />
                                                        </td>
                                                        <td />
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            Total SUNAT (Sin DUA)
                                                        </td>
                                                        <td class="input">
                                                            <input id="txtTotSUNAT" type="text" class="css_input" runat="server" />
                                                        </td>
                                                        <td />
                                                        <!--
										    <td class="label">
											    Retenciones
										    </td>
										    <td class="input">
											    <input id="txtTotRet" type="text" class="css_input" runat="server" />
										    </td>
										    <td class="label">
											    Detracciones
										    </td>
										    <td class="input">
											    <input id="txtTotDet" type="text" class="css_input" runat="server" />
										    </td>
										    <td class="label">
											    Renta 4ta
										    </td>
										    <td class="input">
											    <input id="txtTot4ta" type="text" class="css_input" runat="server" />
										    </td>
										    -->
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <!-- Fin IBK - AAE - Agrego sumarizacion-->
                                        <!-- DIFERENCIA TIPO CAMBIO -->
                                        <tr id="tr_difCambio">
                                            <td style="width: 10px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                onclick="javascript:fn_util_desplegar('grd_difCambio','imgMaxiDifCambio','imgMiniDifCambio');fn_util_desplegar('tot_diferencia','imgMaxiDifCambio','imgMiniDifCambio');">
                                                <img id="imgMaxiDifCambio" src="../Util/images/ico_segMaximizar.gif" style="" />
                                                <img id="imgMiniDifCambio" src="../Util/images/ico_segMinimizar.gif" style="display: none;" />
                                            </td>
                                            <td colspan="2" class="css_lbl_subTitulo" style="cursor: pointer; background-color: #EFEFEF;
                                                border-bottom: 3px solid #ffffff;" onclick="javascript:fn_util_desplegar('grd_difCambio','imgMaxiDifCambio','imgMiniDifCambio');fn_util_desplegar('tot_diferencia','imgMaxiDifCambio','imgMiniDifCambio');">
                                                Diferencia de Cambio
                                            </td>
                                        </tr>
                                        <tr id="grd_difCambio" style="display: none;">
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td colspan="2">
                                                <table id="jqGrid_lista_E">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_E" style="display: none;">
                                                </div>
                                                <br />
                                            </td>
                                        </tr>
                                        <!-- Inicio IBK - AAE - Agrego sumarizacion-->
                                        <tr id="tot_diferencia" style="display: none;">
                                            <td colspan="3">
                                                <table cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="label">
                                                            Total Diferencias en moneda del contrato
                                                        </td>
                                                        <td class="input">
                                                            <input id="txtTotDif" type="text" class="css_input" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <!-- Fin IBK - AAE - Agrego sumarizacion-->
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Cargos</legend>
                                    <br />
                                    <table cellpadding="4" cellspacing="0" border="0" width="970px">
                                        <!-- Cargos -->
                                        <tr id="tr_cargos">
                                            <td style="width: 10px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                onclick="javascript:fn_util_desplegar('grd_cargos', 'imgMaxiCargos', 'imgMiniCargos');">
                                                <img id="imgMaxiCargos" src="../Util/images/ico_segMaximizar.gif" />
                                                <img id="imgMiniCargos" src="../Util/images/ico_segMinimizar.gif" style="display: none;" />
                                            </td>
                                            <td style="width: 830px; cursor: pointer; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;"
                                                class="css_lbl_subTitulo" onclick="javascript:fn_util_desplegar('grd_cargos', 'imgMaxiCargos', 'imgMiniCargos');">
                                                Cargos
                                            </td>
                                            <td style="width: 130px; text-align: right; background-color: #EFEFEF; border-bottom: 3px solid #ffffff;">
                                                <a href="javascript:fn_abreNuevoCargo();" id="af_linkAbreCargo">
                                                    <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                        border="0" align="bottom" />
                                                    Agregar Cargo </a>
                                            </td>
                                        </tr>
                                        <tr id="grd_cargos" style="display: none;">
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td colspan="2">
                                                <table id="jqGrid_lista_F">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_F" style="display: none;">
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
                                                                <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                                    border="0" align="bottom" />Eliminar </a>
                                                        </div>
                                                        <div id="dvBotonEditaComentario" class="dv_img_boton_mini" style="border: 0px">
                                                            <a href="javascript:fn_editarDocumentoComentario();">
                                                                <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                                    border="0" align="bottom" />Editar </a>
                                                        </div>
                                                        <div id="dvBotonagregaComentario" class="dv_img_boton_mini" style="border: 0px">
                                                            <a href="javascript:fn_abreNuevoDocumentoComentario();">
                                                                <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                                    border="0" align="bottom" />Agregar </a>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--Inicia Carga Grilla -->
                                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 850px">
                                                <tr>
                                                    <td>
                                                        <table id="jqGrid_lista_G">
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
                            <!-- Inicio IBK - AAE - Tab Activacion -->
                            <!-- ******************************* -->
                            <!-- TAB :: Activacion -->
                            <!-- ******************************* -->
                            <div id="tab-3" style="height: 100%;">
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Datos Cronograma</legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td colspan="6" style="text-align: right; padding-right: 10px">
                                                <div id="dv_Modificar" class="dv_img_boton_mini" style="width: 80px; border: 0px solid #ffffff;"
                                                    runat="server">
                                                    <a href="javascript:fn_ExportarExcel();">
                                                        <img alt="" src="../Util/images/ico_excel_32x32.png" style="width: 16px; height: 16px;"
                                                            border="0" />
                                                        Exportar </a>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="jqGrid_lista_H">
                                        <tr>
                                            <td />
                                        </tr>
                                    </table>
                                    <div id="jqGrid_pager_H">
                                    </div>
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <colgroup>
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Precio Venta
                                            </td>
                                            <td class="input">
                                                <input id="txtPrecioVentaCro" name="txtPrecioVentaCro" type="text" class="css_input"
                                                    value="" size="15" runat="server" />
                                            </td>
                                            <td class="label">
                                                IGV
                                            </td>
                                            <td class="input">
                                                <input id="txtMontoIGVCro" name="txtMontoIGVCro" type="text" class="css_input" value=""
                                                    size="10" runat="server" />
                                            </td>
                                            <td class="label">
                                                Valor Venta
                                            </td>
                                            <td class="input">
                                                <input id="txtValorVentaCro" name="txtValorVentaCro" type="text" class="css_input"
                                                    value="" size="15" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <span>Cuota Inicial </span>
                                            </td>
                                            <td class="input">
                                                <input id="txtCuotaInicialCro" name="txtCuotaInicialCro" type="text" class="css_input"
                                                    value="" size="15" runat="server" />
                                            </td>
                                            <td class="label">
                                                Riesgo Neto
                                            </td>
                                            <td class="input">
                                                <input id="txtRiesgoNetoCro" name="txtRiesgoNetoCro" type="text" class="" value=""
                                                    size="15" runat="server" />
                                            </td>
                                            <td class="label">
                                                T.E.A. %
                                            </td>
                                            <td class="input">
                                                <input id="txtTEA" name="txtTEA" type="text" class="css_input" value="" size="15"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Tipo de Cronograma
                                            </td>
                                            <td class="input">
                                                <select id="cmbTipoCronograma" name="cmbTipoCronograma" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                            </td>
                                            <td class="label">
                                                Nro. Cuotas
                                            </td>
                                            <td class="input">
                                                <input id="txtNroCuotas" name="txtNroCuotas" type="text" class="css_input" value=""
                                                    size="11" runat="server" />
                                            </td>
                                            <td class="label">
                                                Periodicidad
                                            </td>
                                            <td class="input">
                                                <select id="cmbPeriodicidad" name="cmbPeriodicidad" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Frecuencia de Pago
                                            </td>
                                            <td class="input">
                                                <select id="cmbFrecuenciaPago" name="cmbFrecuenciaPago" runat="server">
                                                    <option value="0">-Seleccionar-</option>
                                                </select>
                                            </td>
                                            <td class="label">
                                                Plazo Gracia
                                            </td>
                                            <td class="input">
                                                <input id="txtPlazoGracia" name="txtPlazoGracia" type="text" class="css_input" value=""
                                                    size="3" runat="server" />&nbsp;cuotas
                                            </td>
                                            <td class="label">
                                                Tipo de Gracia
                                            </td>
                                            <td class="input">
                                                <select id="cmbTipoGracia" name="cmbTipoGracia" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Fecha Activación
                                            </td>
                                            <td class="input">
                                                <input id="txtFechaMaxActivacion" name="txtFechaMaxActivacion" type="text" class="css_input"
                                                    value="" size="11" runat="server" />
                                            </td>
                                            <td class="label">
                                                Fecha 1º Vencimiento
                                            </td>
                                            <td class="input">
                                                <input id="txtFechavence" name="txtFechavence" type="text" class="css_input" value=""
                                                    size="11" runat="server" />
                                            </td>
                                            <td class="label">
                                                Cuota Inicial Contrato
                                            </td>
                                            <td class="input">
                                                <input id="txtCuotaIniContrato" name="txtCuotaIniContrato" type="text" class="css_input"
                                                    value="" size="15" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                            </td>
                                            <td class="input">
                                            </td>
                                            <td class="label">
                                            </td>
                                            <td class="input">
                                            </td>
                                            <td>
                                            </td>
                                            <td class="input">
                                                <div id="dv_botonCronograma">
                                                    <a href="javascript:fn_generaCronograma();" title="Generar Cronograma">
                                                        <img id="img1" alt="" src="../Util/images/ico_genCro.ico" style="cursor: pointer;
                                                            vertical-align: middle;" runat="server" />
                                                    </a>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Seguro Bien</legend>
                                    <table id="Table1" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <colgroup>
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Tipo de Seguro
                                            </td>
                                            <td class="input">
                                                <select id="cmbTipoBienSeguro" name="cmbTipoBienSeguro" runat="server">
                                                    <option value="0">-Seleccionar-</option>
                                                </select>
                                            </td>
                                            <td class="label">
                                                Importe Prima
                                            </td>
                                            <td class="input">
                                                <input id="txtImportePrimaSeguroBien" name="txtImportePrimaSeguroBien" type="text"
                                                    class="css_input" value="" size="15" runat="server" />
                                            </td>
                                            <td class="label">
                                                Nº Cuotas a Financiar
                                            </td>
                                            <td class="input">
                                                <input id="txtNumCuotasfinanciadas" name="txtNumCuotasfinanciadas" type="text" class="css_input"
                                                    value="" size="15" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset id="fld_SeguroDegravamen">
                                    <legend class="css_lbl_subTitulo">Seguro Desgravamen</legend>
                                    <table id="Table2" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <colgroup>
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                            <col style="width: 16.66%" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Tipo de Seguro
                                            </td>
                                            <td class="input">
                                                <select id="cmbTipoSeguro" name="cmbTipoSeguro" runat="server">
                                                    <option value="0">-Seleccionar-</option>
                                                </select>
                                            </td>
                                            <td class="label">
                                                Importe Prima
                                            </td>
                                            <td class="input">
                                                <input id="txtImportePrimaDesgravamen" name="txtImportePrimaDesgravamen" type="text"
                                                    class="css_input" value="" size="15" runat="server" />
                                            </td>
                                            <td class="label">
                                                Nº Cuotas a Financiar
                                            </td>
                                            <td class="input">
                                                <input id="txtNumCuotaFinanciar" name="txtNumCuotaFinanciar" type="text" class="css_input"
                                                    value="" size="15" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <!-- ****************** -->
                            <!-- TAB :: CRONOGRAMA  -->
                            <!-- ****************** -->
                            <div id="tab-4">
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                    <tr>
                                        <td id="tbCronograma" style="text-align: center">
                                            <table id="jqGrid_lista_I">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div id="jqGrid_pager_I">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- Fin IBK -->
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!-- Fin Cuerpo -->
    </div>    
    <div style="display: none">
        <asp:Button ID="btnGenerar" runat="server" />
        <asp:Button ID="btnExcelProveedores" runat="server" />
    </div>
    </form>
    
</body>
</html>
