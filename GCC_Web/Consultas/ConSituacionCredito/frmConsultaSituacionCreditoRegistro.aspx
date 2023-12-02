<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaSituacionCreditoRegistro.aspx.vb"
    Inherits="ConSituacionCredito_frmConsultaSituacionCreditoRegistro" %>

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

    <script type='text/javascript' src="../../Util/js/jquery/jquery-1.6.2.min.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"></script>

    <script type="text/javascript" src="../../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.modal.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.date.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"></script>

    <script src="../../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>

    <script src="../../Util/js/jquery/jshashtable.js" type="text/javascript"></script>

    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script src="frmConsultaSituacionCreditoRegistro.aspx.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmContratoRegistro" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!--
        BOTONES
        -->
        <table id="tb_cuerpoCabecera" style="border: 0;" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consultas</div>
                    <div class="css_lbl_titulo">
                        Situación del Crédito :: Detalle</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_Volver" class="dv_img_boton">
                        <a href="javascript:fn_Cancelar();">
                            <img alt="Volver" src="../../Util/images/ico_acc_cancelar.gif" style="border: 0"
                                title="Volver" /><br />
                            Volver </a>
                    </div>
                    <div id="dv_btnSeguimiento" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_abreSeguimiento();">
                            <img alt="" src="../../Util/images/ico_seguimiento.gif" style="border: 0" /><br />
                            Seguimiento </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <%--  --%>
            <table id="tb_tabla_comun" cellpadding="0" cellspacing="0" style="width: 100%; border: 0;">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <!--
        				CABECERA DEL CONTRATO
        				-->
                        <table id="tb_tabla_comunCabecera" cellpadding="0" cellspacing="0" style="border: 0;">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contrato
                                </td>
                                <td class="botones">
                                    <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div style="display: none">
                            <textarea id="TextArea1" cols="200" rows="15" style="width: 600px;"></textarea>
                            <input id="Button1" type="button" value="button" onclick="go();" /></div>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                <colgroup>
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                </colgroup>
                                <tr>
                                    <td class="label">
                                        Nº Crédito
                                    </td>
                                    <td class="input" id="txtNroContrato" runat="server">
                                    </td>
                                    <td class="label">
                                        Tipo de Documento
                                    </td>
                                    <td class="input" id="txtTipoDocumento" runat="server">
                                    </td>
                                    <td class="label">
                                        Nº de Documento
                                    </td>
                                    <td class="input" id="txtNroDeDocumento" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Nombre / Razon Social
                                    </td>
                                    <td class="input" id="txtNombreRazonSocial" runat="server">
                                    </td>
                                    <td class="label">
                                        Banca
                                    </td>
                                    <td class="input" id="txtBanca" runat="server">
                                    </td>
                                    <td class="label">
                                        Segmento
                                    </td>
                                    <td class="input" id="txtSegmento" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Ejecutivo Negocio
                                    </td>
                                    <td class="input" id="txtEjecutivoNegocio" runat="server">
                                    </td>
                                    <td class="label">
                                        Ejecutivo Leasing
                                    </td>
                                    <td class="input" id="txtEjecutivo" runat="server">
                                    </td>
                                    <td class="label">
                                        Fecha Activación
                                    </td>
                                    <td class="input" id="txtFechaActivacion" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Categoria
                                    </td>
                                    <td class="input" id="txtCategoria" runat="server">
                                    </td>
                                    <td class="label">
                                        Producto
                                    </td>
                                    <td class="input" id="txtProducto" runat="server">
                                    </td>
                                    <td class="label">
                                        SubProducto
                                    </td>
                                    <td class="input" id="txtSubProducto" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Estado Crédito
                                    </td>
                                    <td class="input" id="txtEstadoCredito" runat="server">
                                    </td>
                                    <td class="label">
                                        Situación Credito
                                    </td>
                                    <td class="input" id="txtSituacionCredito" runat="server">
                                    </td>
                                    <td class="label">
                                        Tipo Exposición
                                    </td>
                                    <td class="input" id="txtTipoExposicion" runat="server">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--
        				INICIA TABS
        				-->
                        <div id="divTabs" style="border: 0; background: none;" class="dv_tabla_contenedora">
                            <ul>
                                <li><a href="#tab-6">GENERAL</a></li>
                                <li><a href="#tab-0">CRONOGRAMA</a></li>
                                <li><a href="#tab-1">DOCUMENTOS</a></li>
                                <li><a href="#tab-2">BIENES/ACTIVOS</a></li>
                                <li><a href="#tab-3">CONTRATOS Y ADENDAS</a></li>
                                <li><a href="#tab-4">GASTOS</a></li>
                            </ul>
                            <!--
        			         TAB :: GENERAL 
        			         !-->
                            <div id="tab-6">
                                <div id="dv_datosCliente" style="border: 0px;">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <colgroup>
                                            <col style="width: 10%;" />
                                            <col style="width: 10%;" />
                                            <col style="width: 16%;" />
                                            <col style="width: 16%;" />
                                            <col style="width: 20%;" />
                                            <col style="width: 20%;" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Clasificación Bien
                                            </td>
                                            <td class="input" id="txtClasificacion" runat="server">
                                            </td>
                                            <td class="label">
                                                Tipo del Bien
                                            </td>
                                            <td class="input" id="txtTipoBien" runat="server">
                                            </td>
                                            <td class="input">
                                            </td>
                                            <td class="input">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="input">
                                            </td>
                                            <td>
                                            </td>
                                            <td class="input">
                                            </td>
                                            <td>
                                            </td>
                                            <td class="input">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                F. primer desembolso
                                            </td>
                                            <td class="input" id="txtFPrimerDesembolso" runat="server">
                                            </td>
                                            <td class="label">
                                                Moneda
                                            </td>
                                            <td class="input" id="txtMoneda" runat="server">
                                            </td>
                                            <td class="label">
                                                Nro Coutas
                                            </td>
                                            <td class="input" id="txtNroCuotas" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                F. venc. primera cuota
                                            </td>
                                            <td class="input" id="txtFVencPrimeraCuota" runat="server">
                                            </td>
                                            <td class="label">
                                                TEA
                                            </td>
                                            <td class="input" id="txtTea" runat="server">
                                            </td>
                                            <td class="label">
                                                Cuotas Pagadas
                                            </td>
                                            <td class="input" id="txtCuotasPagadas" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                F. venc. siguiente cuota
                                            </td>
                                            <td class="input" id="txtFVencSiguienteCuota" runat="server">
                                            </td>
                                            <td class="label">
                                                Desembolsado
                                            </td>
                                            <td class="input" id="txtDesembolsado" runat="server">
                                            </td>
                                            <td class="label">
                                                Pendientes vigentes
                                            </td>
                                            <td class="input" id="txtPendientesVigentes" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                F. venc. crédito
                                            </td>
                                            <td class="input" id="txtFVencCredito" runat="server">
                                            </td>
                                            <td class="label">
                                                Cuota Inicial
                                            </td>
                                            <td class="input" id="txtCuotaInicial" runat="server">
                                            </td>
                                            <td class="label">
                                                Pendientes vencidas
                                            </td>
                                            <td class="input" id="txtPendientesVencidas" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Principal pagadas cuotas
                                            </td>
                                            <td class="input" id="txtPrincipalPagadas" runat="server">
                                            </td>
                                            <td class="label" id="td19">
                                                Saldo principal
                                            </td>
                                            <td class="input" id="txtSaldoPrincipal" runat="server">
                                            </td>
                                            <td class="label" id="">
                                                Deuda por cobrar
                                            </td>
                                            <td class="input" id="txtDeudarCobrar" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Opción de compra (inc. IGV)
                                            </td>
                                            <td class="input" id="txtOpcionCompra" runat="server">
                                            </td>
                                        </tr>
                                    </table>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Seguro</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <col style="width: 14%;" />
                                            <col style="width: 14%;" />
                                            <col style="width: 16%;" />
                                            <col style="width: 16%;" />
                                            <col style="width: 20%;" />
                                            <col style="width: 20%;" />
                                            <tr>
                                                <td class="label">
                                                    Tipo Seguro
                                                </td>
                                                <td class="input" id="txtTipoSeguro" runat="server">
                                                </td>
                                                <td class="label">
                                                    F. fin vigencia
                                                </td>
                                                <td class="input" id="txtFecFinVigencia">
                                                </td>
                                                <td class="label">
                                                    F. inicio vigencia
                                                </td>
                                                <td class="input" id="txtFecInicioVigencia" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Importe Prima
                                                </td>
                                                <td class="input" id="txtImportePrima" runat="server">
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Cuentas Cliente</legend>
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="5" style="width: 100%;">
                                            <tr>
                                                <td class="label">
                                                    Tipo de Cuenta 1
                                                </td>
                                                <td class="input">
                                                    <select id="cmbTipoCuenta1" name="cmbTipoCuenta1" runat="server">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Moneda
                                                </td>
                                                <td class="input">
                                                    <select id="cmbMoneda1" name="cmbMoneda1" runat="server">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Nº Cuenta
                                                </td>
                                                <td class="input">
                                                    <input id="txtNumeroCuenta1" name="txtNumeroCuenta1" type="text" class="css_input"
                                                        size="20" value="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tipo de Cuenta 2
                                                </td>
                                                <td class="input">
                                                    <select id="cmbTipoCuenta2" name="cmbTipoCuenta2" runat="server">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Moneda
                                                </td>
                                                <td class="input">
                                                    <select id="cmbMoneda2" name="cmbMoneda2" runat="server">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Nº Cuenta
                                                </td>
                                                <td class="input">
                                                    <input id="txtNumeroCuenta2" name="txtNumeroCuenta2" type="text" class="css_input"
                                                        size="20" value="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tipo de Cuenta 3
                                                </td>
                                                <td class="input">
                                                    <select id="cmbTipoCuenta3" name="cmbTipoCuenta3" runat="server">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Moneda
                                                </td>
                                                <td class="input">
                                                    <select id="cmbMoneda3" name="cmbMoneda3" runat="server">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Nº Cuenta
                                                </td>
                                                <td class="input">
                                                    <input id="txtNumeroCuenta3" name="txtNumeroCuenta3" type="text" class="css_input"
                                                        size="20" value="" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <!-- ****************** -->
                            <!-- TAB :: CRONOGRAMA  -->
                            <!-- ****************** -->
                            <div id="tab-0">
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Datos Cronograma</legend>
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
                                                Tipo de Cronograma
                                            </td>
                                            <td class="input" id="txtTipoCronograma" runat="server">
                                            </td>
                                            <td class="label">
                                                Nro. Cuotas
                                            </td>
                                            <td class="input" id="txtNroCuotasCronograma" runat="server">
                                            </td>
                                            <td class="label">
                                                Periodicidad
                                            </td>
                                            <td class="input" id="txtPeriodicidad" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Frecuencia de Pago
                                            </td>
                                            <td class="input" id="txtFrecuenciaPago" runat="server">
                                            </td>
                                            <td class="label">
                                                Tipo de Gracia
                                            </td>
                                            <td class="input" id="txtTipoGracia" runat="server">
                                            </td>
                                            <td class="label">
                                                Plazo Gracia
                                            </td>
                                            <td class="input" id="txtPlazoGracia" runat="server">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Tasa(TEA)
                                            </td>
                                            <td class="input" id="txtTir" runat="server">
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                    <tr>
                                        <td id="tbCronograma" style="text-align: center">
                                            <table id="jqGrid_lista_L">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div id="jqGrid_pager_L">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--
                               <!--
        			         TAB :: COMPROBANTES
        			         !-->
                            <div id="tab-1">
                                <div class="dv_tabla_contenedora">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td>
                                                <table id="jqGrid_lista_E">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_E">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!--
        			         TAB :: BIENES/ACTIVOS
        			         !-->
                            <div id="tab-2">
                                <!-- Datos Inmueble -->
                                <div id="dvDatosBien" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosInmueble" border="0">
                                            <tr>
                                                <td>
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
                                <!-- Datos Maquinaria -->
                                <div id="dvDatosMaquinaria" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosMaquinaria">
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_B">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_B">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <!-- Datos UTT -->
                                <div id="dvDatosUTT" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosUTT">
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_C">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_C">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <!-- Datos Otros -->
                                <div id="dvDatosOtros" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosOtros">
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_J">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_J">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <!--
        			         TAB :: CONTRATOS Y ADENDAS
        			         !-->
                            <div id="tab-3">
                                <div class="dv_tabla_contenedora">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <colgroup>
                                            <col style="width: 16.66%;" />
                                            <col style="width: 16.66%;" />
                                            <col style="width: 16.66%;" />
                                            <col style="width: 16.66%;" />
                                            <col style="width: 16.66%;" />
                                            <col style="width: 16.66%;" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Nº Contrato
                                            </td>
                                            <td class="input" id="txtNumContrato" runat="server">
                                            </td>
                                            <td class="label">
                                                Razon Social o Nombre
                                            </td>
                                            <td class="input" id="txtRazonSocial" runat="server">
                                            </td>
                                            <td class="label">
                                                Tipo Bien
                                            </td>
                                            <td class="input" id="txtTipoContrato" runat="server">
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
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
                            </div>
                            <!--
        			         TAB :: GASTOS
        			         !-->
                            <div id="tab-4">
                                <div id="dv_datosNotariales" style="border: 0px;">
                                    <fieldset>
                                        <input id="hdnCodigoNotarial" type="hidden" />
                                        <table style="border: 0;">
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_G">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_G">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <input type="hidden" id="hddVersionCotizacion" name="hddVersionCotizacion" runat="server" />
        <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" />
        <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" />
        <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" />
        <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" />
        <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" />
        <input type="hidden" id="hddUbigeoUbicacion" name="hddUbigeoUbicacion" runat="server" />
        <input type="button" name="cmdGuardarRepresentante" id="cmdGuardarRepresentante"
            onclick="javascript:fn_ListaRepresentantesClienteFromModal();" style="display: none;" />
        <input id="hddCodigoCotizacion" type="hidden" runat="server" />
        <input id="hddCorreocontacto" type="hidden" runat="server" />
        <input id="hdnCodigoAdenda" type="hidden" />
        <input id="hddCodProductoFinancieroActivo" type="hidden" runat="server" />
        <input id="hddCodMoneda" type="hidden" runat="server" />
        <input id="hddTipoDocumentoConyuge" type="hidden" runat="server" />
        <input id="hddCodigoTipoPersona" type="hidden" runat="server" />
        <input id="hddTipoDocumento" type="hidden" runat="server" />
        <input id="hddCodigoEstadoCivil" type="hidden" runat="server" />
        <input id="hddEstadoCivil" type="hidden" runat="server" />
        <input id="hddCodigoContrato" type="hidden" runat="server" />
        <input id="hddTipoRubroFinanciamiento" type="hidden" runat="server" />
        <input id="hddCodigoClasificacionContrato" type="hidden" runat="server" />
        <input id="hddCodigoTipoBien" type="hidden" runat="server" />
        <input id="hddSecFinanciamiento" type="hidden" />
        <input id="hddRowId" type="hidden" />
        <input id="hddCodProveedor" type="hidden" />
        <input id="hddCodigoEstadoBien" type="hidden" runat="server" />
        <input id="hddCodigoEstadoContrato" type="hidden" runat="server" />
        <input id="hddFechaFirmaNotaria" type="hidden" runat="server" />
        <input id="hddClasifContratoSeleccion" type="hidden" runat="server" />
        <!-- Datos del bien -->
        <!-- Detecta cambios en el contenido de los controles, antes de salir de la actual ventana. -->
        <input id="hddCambiosSinGuardar" type="hidden" />
        <!-- Indica si alguno de los datos del contrato o de otros documentos han cambiado y requieren volver a generar el contrato.
             El cambio se registra a través de la base de datos -->
        <input id="hddFlagModificado" type="hidden" runat="server" />
        <input id="hddMensajeCorreo" type="hidden" runat="server" />
        <input id="hddFechaActual" type="hidden" runat="server" />
        <!-- Datos del bien -->
        <input id="hddUso" type="hidden" runat="server" />
        <input id="hddUbicacion" type="hidden" runat="server" />
        <!-- Contrato (Anexos) -->
        <input id="btnAdjuntarArchivo" type="button" runat="server" onclick="javascript:fn_ActualizarAnexo();"
            style="visibility: hidden; display: none;" />
        <!-- Contrato Valida Modificaciones -->
        <input id="hddAdjuntarArchivo" type="hidden" runat="server" />
        <input id="hddValidaModificacion" type="hidden" />
        <input id="hddGeneraContrato_Adjunto" type="hidden" />
        <!-- Datos del conyugue -->
        <input id="btnAdjuntarArchivoDocumentoSeparacion" type="button" runat="server" onclick="javascript:fn_ActualizarDocumentoSeparacion();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoDocumentoSeparacion" type="hidden" runat="server" />
        <!-- Otros conceptos -->
        <input id="btnAdjuntarArchivoOtroConcepto" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoAdjunto();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoOtroConcepto" type="hidden" runat="server" />
        <!-- Adenda - nuevo -->
        <input id="btnAdjuntarArchivoNotarialNuevo" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoNotarialNuevo();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoNotarialNuevo" type="hidden" runat="server" />
        <!-- Adenda - editar -->
        <input id="btnAdjuntarArchivoNotarialEditar" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoNotarialEditar();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoNotarialEditar" type="hidden" runat="server" />
        <!-- Texto predefinido -->
        <input id="btnTextoPredefinido" type="button" runat="server" onclick="javascript:fn_ActualizarTextoPredefinido();"
            style="visibility: hidden; display: none;" />
        <input id="hddTextoPredefinido" type="hidden" runat="server" />
        <!-- Retorno -->
        <input type="hidden" id="hddFlagRetorno" name="hddFlagRetorno" value="" runat="server" />
    </div>
    </form>
</body>
</html>
