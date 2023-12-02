<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGastoContratoRegistro.aspx.vb"
    Inherits="OtrosConceptos_frmGastoContratoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gasto Contrato </title>
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

    <script src="../../Util/js/jquery/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>

    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <!-- JQGrid -->

    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmGastoContratoRegistro.aspx.js"> </script>

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
                        Gastos:: Contrato Registro</div>
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
                <tr>
                    <td class="cuerpo">
                        <table id="tb_Contrato" border="0" cellpadding="3" cellspacing="0">
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
                                        Nro Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" name="txtNroContrato" type="text" class="css_input" size="18"
                                            onblur='javascript:fn_buscarContrato();' runat="server" /><img id="imgBuscarContrato"
                                                alt="" src="../../Util/images/ico_buscar.jpg" style="cursor: pointer; vertical-align: middle;"
                                                onclick="javascript:fn_VentanaContrato();" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;</td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input" colspan="5">
                                        <input id="txtRazonSocialProveedor" name="txtRazonSocialProveedor" type="text" class="css_input_inactivo"
                                            size="70" disabled="disabled" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Comisión de Act.
                                    </td>
                                    <td class="input">
                                        <input id="txtComisionAct" name="txtComisionAct" type="text" class="css_input" size="15"
                                            runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="label">
                                        Saldo de Com. Act.
                                    </td>
                                    <td class="input">
                                        <input id="txtSaldoComisionAct" name="txtSaldoComisionAct" type="text" class="css_input"
                                            size="15" runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Asumido Por
                                    </td>
                                    <td class="input">
                                        BANCO
                                        <asp:RadioButton ID="rbBanco" runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        CLIENTE
                                        <asp:RadioButton ID="rbCliente" runat="server" />
                                    </td>
                                    <td class="input">
                                        COBRABLE
                                        <asp:RadioButton ID="rbCobrable" runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Concepto
                                    </td>
                                    <td class="input" colspan="4">
                                        <select id="cboConcepto" runat="server" name="cboConcepto">
                                            <option value="0">[-Seleccione-]</option>
                                            <option value="1">ASESORIA LEGAL EXTERNA</option>
                                            <option value="2">ASESORIA LEGAL EXTERNA -HONORARIOS</option>
                                            <option value="3">GASTO DE SEGURO VEHICULAR</option>
                                            <option value="4">GASTOS NOTARIALES</option>
                                            <option value="5">GASTOS REGISTRALES</option>
                                            <option value="6">DIFERIDO POLIZA DE SEGURO DE ARRENDAMIENTO FINANCIERO</option>
                                            <option value="7">COMISIONISTAS LEASING</option>
                                            <option value="8">COMISIONISTAS LEASING CON FACTURA</option>
                                            <option value="9">GASTOS DE TASACIONES</option>
                                            <option value="10">GASTO NOTARIAL EXONERADO IGV-SELVA</option>
                                        </select>
                                        &nbsp; &nbsp; &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <input id="txtMoneda" name="txtMoneda" type="text" class="css_input_inactivo" size="20"
                                            disabled="disabled" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Importe
                                    </td>
                                    <td class="input">
                                        <input id="txtImporte" name="txtImporte" type="text" class="css_input" size="15"
                                            runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Comisión
                                    </td>
                                    <td class="input">
                                        <input id="txtComision" name="txtComision" type="text" class="css_input" size="15"
                                            runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="label">
                                        IGV Comisión
                                    </td>
                                    <td class="input">
                                        <input id="txtIGVComision" name="txtIGVComision" type="text" class="css_input" size="15"
                                            runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Total
                                    </td>
                                    <td class="input">
                                        <input id="txtTotal" name="txtTotal" type="text" class="css_input" size="15"
                                            runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="label">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Fecha Cobro
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaCobro" name="txtFechaCobro" type="text" class="css_input" size="12"
                                            runat="server" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                    <td class="label">
                                        Estado Cobro
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoCobro" name="txtEstadoCobro" type="text" class="css_input_inactivo"
                                            size="18" disabled="disabled" />
                                    </td>
                                    <td class="input">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnTitulo" runat="server" />
    <asp:HiddenField ID="hddCodContrato" runat="server" />
    <asp:HiddenField ID="hddusuariosesion" runat="server" />
    </form>
</body>
</html>
