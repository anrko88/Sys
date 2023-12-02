<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPrincipal.aspx.vb" Inherits="frmPrincipal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SGL :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="util/images/PV16x16.ico" />
    <!-- JavaScript -->

    <script type='text/javascript' src="Util/js/jquery/jquery-1.6.2.min.js"> </script>

    <script type='text/javascript' src="Util/js/jquery/jquery-ui-1.8.13.custom.min.js"> </script>

    <script type='text/javascript' src="Util/js/jquery/jquery.blockUI.js"> </script>

    <script type='text/javascript' src="Util/js/js_util.modal.js"> </script>

    <script type='text/javascript' src="Util/js/js_util.funcion.js"> </script>

    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="Util/css/css_fuente.css" />
    <!-- Menu -->

    <script type="text/javascript" src="Util/js/jquery/jquery.positionBy.js"></script>

    <script type="text/javascript" src="Util/js/jquery/jquery.bgiframe.js"></script>

    <script type="text/javascript" src="Util/js/jquery/jquery.jdMenu.js"></script>

    <link type="text/css" rel="stylesheet" href="Util/css/jquery/jquery.jdMenu.css" />
    <!-- Local -->

    <script type='text/javascript' src="frmPrincipal.aspx.js"> </script>

</head>
<body>
    <div id="divBody">
        <form id="frmContenedor" runat="server">
        <!-- **************************************************************************************** -->
        <!-- CABECERA -->
        <!-- **************************************************************************************** -->
        <div id="dv_cabecera">
            <table id="tb_cabecera" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="left">
                    </td>
                    <td class="right">
                        <%--Viernes, 09 Febrero del 2012  12:08:26 p.m.--%>
                        <asp:Label ID="lblFecha" runat="server"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblHora" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!-- **************************************************************************************** -->
        <!-- MENU -->
        <!-- **************************************************************************************** -->
        <div id="dv_menu">
            <table id="tb_menu" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 60%">
                        <ul class="jd_menu" id="ULMenu" runat="server">
                            <%--  					
				        <li><a href="javascript:fn_util_menuLink('Comun/frmInicio.aspx');" > Inicio </a></li>
				        --%>
                            <li class="accessible" id="m1" runat="server">Cotización y Negociación
                                <ul>
                                    <li id="m2" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Cotizacion/frmCotizacionListado.aspx');">
                                            Registrar Cotización </a></li>
                                    <li id="m3" runat="server" class="accessible">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="#">
                                            Solicitud de Documentos &raquo; </a>
                                        <ul>
                                            <li id="m4" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Verificacion/frmSolicitudDocumentoClienteListado.aspx');">Cliente</a>
                                            </li>
                                            <li id="m5" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Verificacion/frmSolicitudDocumentoProveedorListado.aspx');">Proveedor</a>
                                            </li>
                                        </ul>
                                    </li>
                                    <li id="m6" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Verificacion/frmCheckListComercialListado.aspx');">
                                            Checklist Comercial </a></li>
                                    <li id="m17" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Verificacion/frmPipelineListado.aspx');">
                                            Pipeline</a> </li>
                                </ul>
                            </li>
                            <li id="m7" runat="server">Formalización
                                <ul>
                                    <li id="m8" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Formalizacion/frmCheckListLegalListado.aspx?q=for');">
                                            Checklist Legal</a> </li>
                                    <li id="m9" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Formalizacion/frmContratoListado.aspx');">
                                            Contrato </a></li>
                                    <li id="m30" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Formalizacion/CesionContrato/frmCesionContratoListado.aspx');">
                                            Cesión de Contrato </a></li>
                                </ul>
                            </li>
                            <li id="m10" runat="server">Desembolso
                                <ul>
                                    <li id="m11" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('desembolso/frmdesembolsoListado.aspx');">
                                            Registro Documentos </a></li>
                                    <!-- Inicio IBK - AAE - Se agregan los faltantes de menu-->
                                    <li id="m18" runat="server">
                                        <!-- Fin IBK - AAE - Se agregan los faltantes de menu-->
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('InsDesembolso/frmInsDesembolsoListado.aspx');">
                                            Instrucción Desembolso </a></li>
                                </ul>
                            </li>
                            <li id="m12" runat="server">Gestión del Bien
                                <ul>
                                    <li id="m31" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/ImpuestoVehicular/frmImpuestoVehicularListado.aspx');">
                                            Impuesto Vehicular </a></li>
                                    <li id="m32" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/MultaVehicular/frmMultaVehicularListado.aspx');">
                                            Multas Vehicular </a></li>
                                    <li id="m33" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleListado.aspx');">
                                            Impuesto y Multas Inmueble </a></li>
                                    <li class="accessible" id="m34" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="#">
                                            Otros Conceptos &raquo; </a>
                                        <ul>
                                            <%--						            <li><img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt=""/><a href="javascript:fn_util_menuLink('GestionBien/OtrosConceptos/frmGastoListado.aspx');"> Registro Gastos </a></li>
										<li><img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt=""/><a href="javascript:fn_util_menuLink('GestionBien/OtrosConceptos/frmInsGastoListado.aspx');"> Instrucción Gastos </a></li>--%>
                                            <li id="m35" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Pagos/frmPagoConceptoListado.aspx');">
                                                    <%-- <a href="javascript:fn_util_menuLink('GestionBien/OtrosConceptos/frmCobroListado.aspx');">--%>
                                                    Registro Cobros </a></li>
                                        </ul>
                                    </li>
                                    <li id="m36" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/Demanda/frmDemandaListado.aspx');">
                                            Demanda </a></li>
                                    <li id="m37" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/Siniestro/frmSiniestroListado.aspx');">
                                            Siniestro </a></li>
                                    <li class="accessible" id="m38" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="#">
                                            Tasación &raquo; </a>
                                        <ul>
                                            <li id="m39" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/Tasacion/frmTasacionAsignacionListado.aspx');">
                                                    Asignación </a></li>
                                            <li id="m40" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/Tasacion/frmTasacionListado.aspx');">
                                                    Registro </a></li>
                                        </ul>
                                    </li>
                                    <li id="m21" runat="server">
                                        <!-- Fin IBK - RPH : Creditos-->
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/frmSegurosListado.aspx');">
                                            Seguros </a></li>
                                    <li id="m13" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Administracion/frmMantenimientoListaContrato.aspx');">
                                            Mantenimiento del Bien </a></li>
                                    <li id="m41" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/OpcionCompra/frmOpcionCompraListado.aspx');">
                                            Opción de Compra </a></li>
                                </ul>
                            </li>
                            <!-- Inicio IBK - RPR - Menu de Pagos -->
                            <li id="m22" runat="server">Pagos
                                <ul>
                                    <li id="m23" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Pagos/frmPagoCuotasListado.aspx');">
                                            Pago de Cuotas </a></li>
                                    <li id="m24" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Pagos/frmPagoConceptoListado.aspx');">
                                            Pago de Conceptos </a></li>
                                    <li class="accessible" id="m42" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="#">
                                            Extornos </a>
                                        <ul>
                                            <li id="m43" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Pagos/frmPagoCuotasListado.aspx?op=E');">
                                                    Pago de Cuotas </a></li>
                                        </ul>
                                    </li>
                                    <li id="m44" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Pagos/frmLiquidacionesListado.aspx');">
                                            Liquidaciones </a></li>
                                </ul>
                            </li>
                            <!-- Fin IBK - RPR - Menu de Pagos-->
                            <li id="m14" runat="server">Mantenimiento
                                <ul>
                                    <li id="m15" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Administracion/frmProveedorListado.aspx');">
                                            Proveedor </a></li>
                                    <!-- Inicio IBK - AAE - Se agregan los faltantes de menu-->
                                    <li id="m16" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Administracion/frmSeguroListado.aspx');">
                                            Seguros </a></li>
                                    <li id="Li1" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Administracion/frmMntClienteListado.aspx');">
                                            Cliente </a></li>
                                    <li id="m57" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Administracion/frmListadoTipoCambio.aspx');">
                                            Tipo Cambio </a></li>
                                    <!-- Fin IBK - AAE - Se agregan los faltantes de menu-->
                                    <%--
						        <li id="m16" runat="server">
						            <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt=""/><a href="javascript:fn_util_menuLink('Administracion/frmSeguroListado.aspx');" > Seguros </a>
						        </li>
						        <li><img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt=""/><a href="javascript:fn_util_menuLink('Temporal/frmTemporalListado.aspx');" > Temporal Listado </a></li>  		
						        --%>
                                    <li id="mxx" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Administracion/frmENegocioListado.aspx');">
                                            Ejecutivos </a></li>
                                </ul>
                            </li>
                            <li id="m25" runat="server">Consultas
                                <!-- Fin IBK - AAE - Se agregan los faltantes de menu-->
                                <ul>
                                    <li id="m45" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Consultas/ConSituacionCredito/frmConsultaSituacionCreditoListado.aspx');">
                                            Situación del Credito </a></li>
                                    <li id="m26" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Consultas/DetalleBien/frmConsultaListaContrato.aspx');">
                                            Detalle de Bien </a></li>
                                    <li id="m46" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Consultas/ImpuestoVehicular/frmConsultaImpuestoVehicular.aspx');">
                                            Impuesto Vehicular </a></li>
                                    <li id="m47" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Consultas/MultaVehicular/frmMultaVehicularListadoConsulta.aspx');">
                                            Multa Vehicular </a></li>
                                    <li id="m48" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Consultas/ImpuestoMultasInmueble/frmConsultaImpuestoMultaInmuebleListado.aspx');">
                                            Impuesto y Multas Inmueble </a></li>
                                    <li id="m49" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Consultas/Siniestro/frmSiniestroListadoConsulta.aspx');">
                                            Siniestro </a></li>
                                    <li id="m50" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Consultas/InscripcionRegistral/frmConsultaInscripcionRegistral.aspx');">
                                            Inscripción Registral </a></li>
                                </ul>
                            </li>
                            <!-- Inicio IBK - AAE - Se agregan los faltantes de menu-->
                            <li id="m19" runat="server">Reportes
                                <!-- Fin IBK - AAE - Se agregan los faltantes de menu-->
                                <ul>
                                    <li id="m20" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmBandejaListado.aspx');">
                                            Bandeja de Estados </a></li>
                                    <li class="accessible" id="m51" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="#">
                                            Reporte SUNAT &raquo; </a>
                                        <ul>
                                            <li id="m27" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('GestionBien/OpcionCompra/frmRepSunatOpcionesCompra.aspx');">
                                                    Opciones de Compra </a></li>
                                            <li id="m28" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Formalizacion/CesionContrato/frmRepSunatCesionesContrato.aspx');">
                                                    Cesiones de Contrato </a></li>
                                            <li id="m52" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmFiltroRepSunatContrato.aspx');">
                                                    Contratos Activados </a></li>
                                            <li id="m53" runat="server">
                                                <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmFiltroRepDetalleBien.aspx');">
                                                    Detalle del Bien </a></li>
                                        </ul>
                                    </li>
                                    <li id="m29" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('InsDesembolso/frmRepDesembolsoMensual.aspx');">
                                            Desembolsos Mensuales </a></li>
                                    <li id="m54" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmFiltroRepOpcionCompra.aspx');">
                                            Opción de Compra </a></li>
                                    <li id="m55" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Formalizacion/frmRepSaldosCredito.aspx');">
                                            Saldos de Crédito </a></li>
                                    <li id="m56" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Formalizacion/frmRepContratosActivados.aspx');">
                                            Contratos Activados </a></li>
                                    <li id="m60" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmConsultaRetenciones.aspx');">
                                            Retenciones </a></li>
                                    <li id="m61" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmRepRetencionCuarta.aspx');">
                                            Retenciones 4ta </a></li>
                                    <li id="m62" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmConsultaRegistroCompra.aspx');">
                                            Registro de Compra </a></li>
                                    <li id="m63" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmReporteVentas.aspx');">
                                            Registro de Venta </a></li>
                                    <li id="m64" runat="server">
                                        <img src="util/images/menu/ico_menu.gif" class="jd_menu_icono" alt="" /><a href="javascript:fn_util_menuLink('Reportes/frmNotasAbono.aspx');">
                                            Nota de Abono </a></li>
                                </ul>
                            </li>
                        </ul>
                    </td>
                    <td class="right" style="width: 40%">
                        <span>
                            <asp:Label ID="lblCodeUser" runat="server"></asp:Label>
                            -
                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            <span class="css_menu_separator">&nbsp;&nbsp;</span>
                            <asp:Label ID="lblDescripRol" runat="server"></asp:Label>
                            <span class="css_menu_separator">&nbsp;&nbsp;</span>
                            <asp:Label ID="lblAmbiente" runat="server"></asp:Label>
                            <span class="css_menu_separator">&nbsp;&nbsp;</span>
                            <asp:LinkButton ID="lkbCerrarSession" CssClass="css_linkCerrarSesion" runat="server">
                        <img alt="" src="util/images/ico_cerrar_sesion.jpg" class="jd_menu_icono"/>Cerrar Sesión
                            </asp:LinkButton>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <table id="tb_menu_separador" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="linea" onclick="javascript:fn_ocultaHeader();">
                </td>
            </tr>
        </table>
        <!-- **************************************************************************************** -->
        <!-- CUERPO -->
        <!-- **************************************************************************************** -->
        <iframe id="ifrm_contenedor" src="Comun/frmInicio.aspx" frameborder="0" scrolling="no">
        </iframe>
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
        <!-- **************************************************************************************** -->
        <!-- PIE -->
        <!-- **************************************************************************************** -->
        <div id="dv_pie" style="display: none;">
            <table id="tb_pie" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="contenido_left">
                        Versión. 2012.02.10
                    </td>
                    <td class="contenido_right">
                        Todos los derechos Reservados © Interbank - TeamSoft
                        <br />
                        GCC - Sistema de Gestión de Leasing
                    </td>
                </tr>
            </table>
        </div>
        <!-- **************************************************************************************** -->
        <!-- DIV Cargando -->
        <!-- **************************************************************************************** -->
        <div id="dv_cargando" style="display: none;">
            <table border="0" cellpadding="5" cellspacing="0" align="center" width="105px">
                <tr>
                    <td>
                        <img alt="" src="util/images/img_ajax.gif" />
                    </td>
                    <td style="font-size: 11px; width: 80px; height: 30px;">
                        Procesando...
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
</body>
</html>
