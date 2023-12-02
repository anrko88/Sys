<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCobroRegistro.aspx.vb"
    Inherits="OtrosConceptos_frmCobroRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cobros Registro </title>
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

    <script type='text/javascript' src="frmCobroRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmObservacion" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <img src="../../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo" id="divTitulo" runat="server">
                        Otros Conceptos
                    </div>
                    <div class="css_lbl_titulo" id="divSubTitulo" runat="server">
                        Otros Conceptos:: Registro</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_CerrarCobro();">
                            <img src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="Div3" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_guardar" class="dv_img_boton">
                        <div id="dv_img_boton" class="dv_img_boton">
                            <a href="javascript:fn_guardar(true,'');">
                                <img src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                                Guardar </a>
                        </div>
                    </div>
                    <div id="dv_Documentos" class="dv_img_boton" style="width: 80px;">
                        <a href="javascript:fn_GBAbreDocumentos();">
                            <img alt="" src="../../Util/images/ico_version.gif" border="0" /><br />
                            Documentos </a>
                    </div>
                </td>
            </tr>
        </table>
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            padding-right: 0px;">
            <tr>
                <td class="lineas">
                </td>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_Contrato" border="0" cellpadding="3" cellspacing="0" width="100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 30px" valign="bottom">
                                    Datos del Contrato
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" width="100%">
                                <tr>
                                    <td class="label">
                                        N° Contrato
                                    </td>
                                    <td class="input">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <input id="txtNroContrato" name="txtNroContrato" type="text" class="css_input" size="12"
                                                        onblur='javascript:fn_buscarContrato();' runat="server" />
                                                </td>
                                                <td>
                                                    <img id="imgBuscarContrato" alt="" src="../../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                                        vertical-align: middle;" onclick="javascript:fn_VentanaContrato();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="label">
                                        Tipo Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtTipoDocumento" name="txtTipoDocumento" runat="server" type="text" class="css_input_inactivo"
                                            size="20" readonly="true" />
                                    </td>
                                    <td class="label">
                                        N° Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocumento" name="txtNroDocumento" runat="server" type="text" class="css_input_inactivo"
                                            size="15" readonly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input" colspan="4">
                                        <input id="txtRazonSocialProveedor" runat="server" name="txtRazonSocialProveedor"
                                            type="text" class="css_input_inactivo" style="width: 380px" onclick="return txtRazonSocialProveedor_onclick()"
                                            readonly="true" />
                                    </td>
                                    <td class="label">
                                        Estado Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoContrato" name="txtEstadoContrato" runat="server" type="text"
                                            class="css_input_inactivo" size="18" readonly="true" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tb_Cobro" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 30px" valign="bottom">
                                    Datos del Cobro
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" width="100%">
                                <tr>
                                    <td class="label">
                                        Concepto
                                    </td>
                                    <td class="input" colspan="4">
                                        <select id="cboConcepto" runat="server" name="cboConcepto" style="width: 400px">
                                        </select>
                                    </td>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <select id="cboMoneda" runat="server" name="cboMoneda">
                                        </select>
                                    </td>
                                </tr>
                                <tr><%--Inicio JJM IBK--%>
                                    <td class="label">
                                        Importe Reembolso
                                    </td>
                                    <td class="input">
                                        <input id="txtImporte" name="txtImporte" style="text-align: right" type="text" class="css_input"
                                            size="15" runat="server" />
                                    </td>
                                    <td class="label">
                                        IGV Reembolso
                                    </td>
                                    <td class="input">
                                        <input id="txtReembolsoIGV" name="txtReembolsoIGV" style="text-align: right" type="text" class="css_input"
                                            size="15" runat="server" />
                                    </td>
                                    <%--Fin JJM IBK--%>
                                    <td class="input">
                                    </td>
                                    <td class="label">
                                        Comisión
                                    </td>
                                    <td class="input">
                                        <input id="txtComision" name="txtComision" style="text-align: right" type="text"
                                            class="css_input" size="15" runat="server" />
                                        <input id="txtConsComision" name="txtConsComision" style="display: none;text-align: right" type="text"
                                            class="css_input_inactivo" readonly="true" size="15" runat="server" />                                            
                                    </td>
                                    <td class="label">
                                        IGV
                                    </td>
                                    <td class="input">
                                        <input id="txtIGVComision" name="txtIGVComision" style="text-align: right" type="text"
                                            class="css_input_inactivo" size="15" runat="server" readonly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Total
                                    </td>
                                    <td class="input">
                                        <input id="txtTotal" name="txtTotal" type="text" style="text-align: right" class="css_input_inactivo"
                                            size="15" runat="server" readonly="true" />
                                    </td>
                                    <td class="input">
                                    </td>
                                    <td class="label">
                                        Fecha Cobro
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaCobro" name="txtFechaCobro" type="text" class="css_calendario"
                                            size="12" runat="server" />
                                        <input id="txtConsFechaCobro" name="txtFechaCobroCons" type="text" class="css_input_inactivo"
                                            size="12" runat="server" readonly="true" style="display: none;" />
                                    </td>
                                    <td class="label">
                                        Estado Cobro
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoCobro" name="txtEstadoCobro" runat="server" type="text" class="css_input_inactivo"
                                            size="18" readonly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Observaciones
                                    </td>
                                    <td class="input" colspan="6">
                                        <textarea id="txtObservaciones" name="txtObservaciones" rows="3" cols="20" style="width: 640px"
                                            runat="server"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <table id="tbPagina" runat="server">
                                        <tr>
                                            <td>
                                                <img id="imgPrevious" alt="" src="../../Util/images/ico_acc_previous.png" style="cursor: pointer;
                                                    vertical-align: middle;" width="16" />
                                            </td>
                                            <td>
                                                Item
                                            </td>
                                            <td>
                                                <input id="txtRegistro" name="txtRegistro" style="text-align: right" type="text"
                                                    class="css_input" size="5" runat="server" />
                                            </td>
                                            <td width="16">
                                                &nbsp;/&nbsp;
                                            </td>
                                            <td id="tdTotal" runat="server">
                                            </td>
                                            <td>
                                                <img id="imgNext" alt="" src="../../Util/images/ico_acc_next.png" style="cursor: pointer;
                                                    vertical-align: middle;" width="16" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tr>
        </table>
    </div>
    <!-- **************************************************************************************** -->
    <!-- DIV LOG -->
    <!-- **************************************************************************************** -->
    <div id="dv_LogAviso" class="cssLogPage_Aviso" style="display: none;">
        <strong>Info &nbsp;:</strong><br />
        <span id="dv_LogAviso_Msg"></span>
    </div>
    <div id="dv_LogError" class="cssLogPage_Error" style="display: none;">
        <strong>Advertencia &nbsp;:</strong><br />
        <span id="dv_LogError_Msg"></span>
    </div>    
    <asp:HiddenField ID="hdnTitulo" runat="server" />
    <asp:HiddenField ID="hidOpcion" runat="server" />
    <asp:HiddenField ID="hidCodSolicitudCredito" runat="server" />
    <asp:HiddenField ID="hidTipoRubroFinanciamiento" runat="server" />
    <asp:HiddenField ID="hidCodIfi" runat="server" />
    <asp:HiddenField ID="hidTipoRecuperacion" runat="server" />
    <asp:HiddenField ID="hidNumSecRecuperacion" runat="server" />
    <asp:HiddenField ID="hidNumSecRecupComi" runat="server" />
    <asp:HiddenField ID="hidCodComisionTipo" runat="server" />
    <asp:HiddenField ID="hidFlagIndividual" runat="server" />
    <asp:HiddenField ID="hidEstadoCobro" runat="server" />
    <asp:HiddenField ID="hidIGV" runat="server" Value="0" />
    <asp:HiddenField ID="hidFilItem" runat="server" Value="" />
    <asp:HiddenField ID="hidFilNroLote" runat="server" Value="" />
    <asp:HiddenField ID="hidFilRazonSocial" runat="server" Value="" />
    <asp:HiddenField ID="hidFilNroContrato" runat="server" Value="" />
    <asp:HiddenField ID="hidFilCUCliente" runat="server" Value="" />
    <asp:HiddenField ID="hidFilConcepto" runat="server" Value="" />
    <asp:HiddenField ID="hidFilEstadoCobro" runat="server" Value="" />
    <asp:HiddenField ID="hidFilSortName" runat="server" Value="" />
    <asp:HiddenField ID="hidFilSortOrder" runat="server" Value="" />
    <asp:HiddenField ID="hidPaginadoAnterior" runat="server" Value="" />
    <asp:HiddenField ID="hidPaginadoSiguiente" runat="server" Value="" />
    <asp:HiddenField ID="hidPaginadoActual" runat="server" Value="" />
    <asp:HiddenField ID="hidTotalRegistros" runat="server" Value="0" />
    <asp:HiddenField ID="hidPressNumero" runat="server" Value="0" />
    <asp:HiddenField ID="hidFechaActual" runat="server" />
    <asp:HiddenField ID="hidEditarCons" runat="server" Value="0" />
    <asp:HiddenField ID="hidInstancia" runat="server" Value="0" />
    <asp:HiddenField ID="hidFechaPago" runat="server" Value="" />
    <asp:HiddenField ID="hidFechaActivacion" runat="server" Value="" />
    <asp:HiddenField ID="hidFechaVencmiento" runat="server" Value="" />
    <asp:HiddenField ID="hidMontoMinimo" runat="server" Value="0" />
    <asp:HiddenField ID="hidMontoMaximo" runat="server" Value="0" />
    <asp:HiddenField ID="hidPorcentajeComisionSC" runat="server" Value="0" />
    <asp:HiddenField ID="hidNumeroSecuencia" runat="server" Value="0" />
    </form>
</body>
</html>

