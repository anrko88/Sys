<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMultaVehicularVer.aspx.vb"
    Inherits="Consultas_MultaVehicular_frmMultaVehicularVer" %>

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

    <script type='text/javascript' src="frmMultaVehicularVer.aspx.js"> </script>

</head>
<body>
    <form id="frmImpuestoVehicularVer" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_multa.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consultas</div>
                    <div class="css_lbl_titulo">
                        Multa Vehicular :: Consulta</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones" style="height: 60px; vertical-align: top;">
                    <div class="dv_img_boton" id="dv_img_boton" style="border: 0">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_documentos" class="dv_img_boton" style="width: 80px;">
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
            </tr>
            <tr>
                <td class="cuerpo">
                    <input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="NUEVO"
                        runat="server" />
                    <input type="hidden" id="hddNumeroContrato" name="hddNumeroContrato" runat="server" />
                    <input type="hidden" id="hddSecFinanciamiento" name="hddSecFinanciamiento" runat="server" />
                    <input type="hidden" id="hddCheck" name="hddCheck" runat="server" />
                    <input type="hidden" id="hddSecImpuesto" name="hddSecImpuesto" runat="server" />
                    <input type="hidden" id="hddCodMunicipalidad" name="hddCodMunicipalidad" runat="server" />
                    <input type="hidden" id="hddOrigen" name="hddOrigen" runat="server" />
                    <input type="hidden" id="hddEstadoCobro" name="hddEstadoCobro" runat="server" />
                    <input type="hidden" id="hddEstadoPago" name="hddEstadoPago" runat="server" />
                    <input type="hidden" id="hddFechaTransferencia" name="hddFechaTransferencia" runat="server" />
                    <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="titulo css_lbl_tituloContenido">
                                Datos del Bien
                            </td>
                        </tr>
                    </table>
                    <div class="dv_tabla_contenedora">
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="label">
                                    Nº Contrato
                                </td>
                                <td class="input" id="tdNroContrato" runat="server">
                                </td>
                                <td class="label">
                                    CU Cliente
                                </td>
                                <td class="input" id="tdCUCliente" runat="server">
                                </td>
                                <td class="label" style="width: 140px">
                                    Razón Social o Nombre
                                </td>
                                <td class="input" id="tdRazonSocial" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    Placa Actual
                                </td>
                                <td class="input" id="tdPlaca" runat="server">
                                </td>
                                <td class="label">
                                    Municipalidad
                                </td>
                                <td class="input" id="tdMunicipalidad" runat="server">
                                </td>
                                <td class="label" style="width: 140px">
                                    Marca
                                </td>
                                <td class="input" id="tdMarca" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    Modelo
                                </td>
                                <td class="input" id="tdModelo" runat="server">
                                </td>
                                <td class="label">
                                    Nº Motor
                                </td>
                                <td class="input" id="tdMotor" runat="server">
                                </td>
                                <td class="label" style="width: 140px">
                                    Nº Lote
                                </td>
                                <td class="input" id="tdNroLote" runat="server">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="titulo css_lbl_tituloContenido">
                                Datos de Multa e Infracción
                            </td>
                        </tr>
                    </table>
                    <div class="dv_tabla_contenedora">
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="label" style="width: 120px">
                                    Nº Infracción
                                </td>
                                <td class="input" id="tdNroInfraccion" runat="server">
                                </td>
                                <td class="label">
                                    Fecha Infracción
                                </td>
                                <td class="input" id="tdFechaInfraccion" runat="server">
                                </td>
                                <td class="label" style="width: 130px">
                                    Concepto
                                </td>
                                <td class="input" id="tdConcepto" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 120px">
                                    Código Infracción
                                </td>
                                <td class="input" id="tdCodigoInfraccion" runat="server">
                                </td>
                                <td class="label">
                                    Fecha Registro
                                </td>
                                <td class="input" id="tdFechaRegistro" runat="server">
                                </td>
                                <td class="label" style="width: 130px">
                                    Fecha Recepción Banco
                                </td>
                                <td class="input" id="tdFechaRecBanco" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 120px">
                                    Importe
                                </td>
                                <td class="input" id="tdImporte" runat="server">
                                </td>
                                <td class="label">
                                    Importe Descuento
                                </td>
                                <td class="input" id="tdImporteDescuento" runat="server">
                                </td>
                                <td class="label" style="width: 130px">
                                    Municipalidad
                                    <select id="ddlMunicipalidad" runat="server" name="ddlMunicipalidad" style="display: none;">
                                        <option value='0'>- Seleccionar -</option>
                                    </select>
                                </td>
                                <td class="input" id="tdMunicipalidadMulta" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 120px">
                                    Pago Cliente
                                </td>
                                <td class="input">
                                    <input id="cbPagoCliente" type="checkbox" runat="server" disabled="disabled" />
                                </td>
                                <td class="label">
                                    Fecha de Pago
                                </td>
                                <td class="input" id="tdFechaPago" runat="server">
                                </td>
                                <td class="label" style="width: 130px">
                                    Estado Pago
                                </td>
                                <td class="input" id="tdEstadoPago" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 120px">
                                    Fecha Cobro
                                </td>
                                <td class="input" id="tdFechaCobro" runat="server">
                                </td>
                                <td class="label">
                                    Estado Cobro
                                </td>
                                <td class="input" id="tdEstadoCobro" runat="server">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 120px">
                                    Observaciones
                                </td>
                                <td class="input" colspan="6">
                                    <textarea id="txtObservaciones" name="txtObservaciones" runat="server" cols="77"
                                        type="text" rows="2" readonly="readonly" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
