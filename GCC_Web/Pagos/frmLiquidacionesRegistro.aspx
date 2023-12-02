<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmLiquidacionesRegistro.aspx.vb"
    Inherits="Pagos_frmLiquidacionesRegistro" %>

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

    <script type='text/javascript' src="../Util/js/jquery/jquery-1.7.2.min.js"> </script>

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
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.4.4.4.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmLiquidacionesRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmPagoCuotasRegistro" runat="server">
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
                        Gestión del Crédito</div>
                    <div class="css_lbl_titulo">
                        Liquidaciones ::
                        <label id="lblOperacion" runat="server">
                        </label>
                    </div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_botonCancelar" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" /><br />
                            Volver </a>
                    </div>
                    <div id="dv_botonEnviar" class="dv_img_boton" style="width: 82px;">
                        <a href="javascript:fn_grabarLiquidacion('1');">
                            <img alt="" src="../Util/images/ico_acc_grabarEnviar.gif" /><br />
                            Enviar </a>
                    </div>
                    <div id="dv_botonGrabar" class="dv_img_boton">
                        <a href="javascript:fn_grabarLiquidacion('0');">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" /><br />
                            Guardar </a>
                    </div>
                    <div id="dv_botonGenerar" class="dv_img_boton">
                        <a href="javascript:fn_generarLiquidacion();" title="Generar Liquidacion">
                            <img alt="" src="../Util/images/ico_acc_cronograma.gif" width="35px" border="0" /><br />
                            Generar </a>
                    </div>
                    <div id="dv_botonEjecutar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_ejecutarLiquidacion();" title="Ejecutar Pago de Cuotas">
                            <img alt="" src="../Util/images/ico_acc_ejecutarID.gif" width="35px" border="0"/><br />
                            Ejecutar </a>
                    </div>
                    <div id="dv_botonAnular" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_anularLiquidacion();" title="Anular">
                            <img alt="" src="../Util/images/ico_acc_devolverID.gif" width="35px" border="0"/><br />
                            Anular </a>
                    </div>
                    <div id="dv_botonDevolver" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_devolverLiquidacion();" title="Devolver">
                            <img alt="" src="../Util/images/ico_acc_devolverID.gif" width="35px" border="0"/><br />
                            Devolver </a>
                    </div>
                    <div id="dv_botonExtornar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_extornarLiquidacion();" title="Extornar">
                            <img alt="" src="../Util/images/ico_acc_devolverID.gif" width="35px" border="0" /><br />
                            Extornar </a>
                    </div>
                    <div id="dv_botonExportar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_Exportar();" title="Exportar">
                            <img alt="" src="../Util/images/ico_acc_detalle.gif" width="35px" border="0"/><br />
                            Exportar </a>
                    </div>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hddPorcIGV" name="hddPorcIGV" value="0.18"
            runat="server" />
        <input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="NUEVO"
            runat="server" />
        <input type="hidden" name="hddError" id="hddError" value="" runat="server" />
        <input type="hidden" name="hddCodSolicitudCredito" id="hddCodSolicitudCredito" value=""
            runat="server" />
        <input type="hidden" name="hddNumSecRecuperacion" id="hddNumSecRecuperacion" value=""
            runat="server" />
        <input type="hidden" name="hddEstadoRecuperacion" id="hddEstadoRecuperacion" value=""
            runat="server" />
        <input type="hidden" name="hddCodMonedaContrato" id="hddCodMonedaContrato" value=""
            runat="server" />
        <input type="hidden" name="hddFlagGenerar" id="hddFlagGenerar" value="0" runat="server" />
        <input type="hidden" name="hddNroCuotasxPagar" id="hddNroCuotasxPagar" value="0"
            runat="server" />
        <input type="hidden" name="hddNroCuotasVencidas" id="hddNroCuotasVencidas" value="0"
            runat="server" />
        <input type="hidden" name="hddNroPagosCuotasxProcesar" id="hddNroPagosCuotasxProcesar"
            value="0" runat="server" />
        <input type="hidden" name="hddNroConceptoPendiente" id="hddNroConceptoPendiente"
            value="0" runat="server" />
        <input type="hidden" name="hddModoVer" id="hddModoVer" value="" runat="server" />
        <input type="hidden" name="hddPerfil" id="hddPerfil" value="" runat="server" />
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
                                            <img id="imgBsqContrato" alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                                vertical-align: middle;" runat="server" />
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
                                            Tipo Persona
                                        </td>
                                        <td class="input">
                                            <input id="txtTipoPersona" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Tipo Documento
                                        </td>
                                        <td class="input">
                                            <input id="txtTipoDocumento" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            N° Documento
                                        </td>
                                        <td class="input" colspan="3">
                                            <input id="txtNumeroDocumento" type="text" class="css_input" size="30" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Tipo Contrato
                                        </td>
                                        <td class="input">
                                            <input id="txtTipoContrato" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Moneda
                                        </td>
                                        <td class="input">
                                            <input id="txtMoneda" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Clasificacion del Bien
                                        </td>
                                        <td class="input">
                                            <input id="txtClasificacionBien" type="text" class="css_input" size="30" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Banca
                                        </td>
                                        <td class="input">
                                            <input id="txtNombreBanca" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Ejecutivo Leasing
                                        </td>
                                        <td class="input">
                                            <input id="txtEjecutivoLeasing" type="text" class="css_input" size="30" runat="server" />
                                        </td>
                                        <td class="label">
                                            Ejecutivo Negocio
                                        </td>
                                        <td class="input">
                                            <input id="txtNombreSectorista" type="text" class="css_input" size="30" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            TEA
                                        </td>
                                        <td class="input">
                                            <input id="txtPorcenTasaActiva" type="text" class="css_input" size="10" style="text-align: right"
                                                runat="server" />
                                            %
                                        </td>
                                        <td class="label">
                                            Estado
                                        </td>
                                        <td class="input">
                                            <input id="txtEstadoContrato" type="text" class="css_input" runat="server" />
                                        </td>
                                        <td class="label">
                                            Situación
                                        </td>
                                        <td class="input">
                                            <input id="txtNombreEstadoOperacionActiva" type="text" class="css_input" size="30"
                                                runat="server" />
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
                                <li><a href="#tab-0">Liquidacion</a></li>
                                <li><a href="#tab-1">Medio de Pago</a></li>
                                <li><a href="#tab-21">Cronograma Original</a></li>
                                <li><a href="#tab-22">Cronograma Nuevo</a></li>
                                <li><a href="#tab-3">Documentos y Comentarios</a></li>
                            </ul>
                            <!-- **************** -->
                            <!-- TAB :: CONSOLIDADO   -->
                            <!-- **************** -->
                            <div id="tab-0">
                                <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="titulo css_lbl_tituloContenido">
                                            Datos de la Liquidación
                                        </td>
                                    </tr>
                                </table>
                                <div id="dv_datosPago" class="dv_tabla_contenedora">
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td class="label">
                                                Nro. Liquidación
                                            </td>
                                            <td class="input">
                                                <input id="txtCodigoLiquidacion" runat="server" />
                                            </td>
                                            <td class="label">
                                                Estado
                                            </td>
                                            <td class="input">
                                                <select id="cmbEstadoLiquidacion" name="cmbEstadoLiquidacion" runat="server">
                                                    <option value="0">-Seleccionar-</option>
                                                </select>
                                            </td>
                                            <td class="label">
                                                Nro. Autorización
                                            </td>
                                            <td class="input">
                                                <input id="txtCodAutorizacionRecuperacion" name="txtCodAutorizacionRecuperacion"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Fecha Valor
                                            </td>
                                            <td class="input">
                                                <input id="txtFechaValor" runat="server" />
                                            </td>
                                            <td class="label">
                                                Fecha Proceso
                                            </td>
                                            <td class="input">
                                                <input id="txtFechaProceso" runat="server" />
                                            </td>
                                            <td class="label">
                                                Tipo de Cambio
                                            </td>
                                            <td class="input">
                                                <select id="cmbTipoCambio" name="cmbTipoCambio" runat="server">
                                                    <option value="0">-Seleccionar-</option>
                                                    <option value="IBK">INTERBANK</option>
                                                    <option value="EMP">EMPRESARIAL</option>
                                                    <option value="PRF">PREFERENCIAL</option>
                                                    <option value="SBS">SUNAT</option>
                                                    <option value="ESP">ESPECIAL</option>
                                                </select>
                                                <input id="txtTipoCambio" style="text-align: right" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Tipo Liquidación
                                            </td>
                                            <td class="input">
                                                <select id="cmbTipoLiquidacion" runat="server">
                                                    <option value="0">-Seleccionar-</option>
                                                </select>
                                            </td>
                                            <td class="label">
                                                Moneda
                                            </td>
                                            <td class="input">
                                                <input id="txtMonedaLiquidacion" runat="server" />
                                            </td>
                                            <td class="label">
                                                Existe Adenda de Contrato
                                            </td>
                                            <td class="input">
                                                <input id="cbFlagAdenda" type="checkbox" runat="server" />
                                            </td>
                                        </tr>
                                        </table>
                                        <table id="tb_formulario">
                                        <tr>
                                            <td>
                                                <div id="dvDatosCronograma">
            						            
       						                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:100%">

					                                <tr>
				                                        <td class="label" >Tipo de Cronograma</td>
			                                            <td class="input" >
			                                                <select id="cmbTipoCronograma" name="cmbTipoCronograma" runat="server" >
			                                                    <option value="0">- Seleccionar -</option>
			                                                </select>
			                                            </td>
			                                            <td class="label" >Nro. Cuotas</td>
			                                            <td class="input" >
			                                                    <input id="txtNroCuotas" name="txtNroCuotas" type="text" class="css_input" value="" size="11" runat="server"/>
			                                            </td>
			                                            <td class="label">Periodicidad</td>
			                                            <td class="input">
		                                                        <select id="cmbPeriodicidad" name="cmbPeriodicidad"  runat="server">
		                                                            <option value="0">- Seleccionar -</option>
		                                                        </select>
			                                            </td>
				                                    </tr>
				                                    <tr> 
				                                         <td class="label" >Frecuencia de Pago</td>
    						                             <td class="input" >
    						                                    <select id="cmbFrecuenciaPago" name="cmbFrecuenciaPago" runat="server">
    						                                        <option value="0">-Seleccionar-</option>        						                     
					                                            </select>
    						                             </td>
    						                             <td class="label">Plazo Gracia</td>
			                                             <td class="input"><input id="txtPlazoGracia" name="txtPlazoGracia" type="text" class="css_input" value="" size="3" runat="server"/>&nbsp;cuotas</td>
			                                             <td class="label">Tipo de Gracia</td>
			                                             <td class="input">
		                                                    <select id="cmbTipoGracia" name="cmbTipoGracia"  runat="server">
		                                                        <option value="0">- Seleccionar -</option>    		                                            
		                                                    </select>
			                                             </td>                               
					                                </tr>
					                                <tr>	                         	
			                                             <td class="label">Fecha 1º Vencimiento</td>
			                                             <td class="input">
			                                                 <input id="txtFechaPrimerVencimiento" name="txtFechaPrimerVencimiento" type="text" class="css_input" value="" size="11" runat="server"/>
			                                             </td>
			                                             
                                                    </tr>					                 					         
                                                </table>
                                                
                                            </div>   
                                            </td>
                                        </tr>
                                        </table>
                                </div>
                                
                                <br />
                                
                                <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="titulo css_lbl_tituloContenido">
                                            Totales
                                        </td>
                                    </tr>
                                </table>
                                <div id="dv_Totales" class="dv_tabla_contenedora">
                                        <table id="tb_formulario">
                                        <tr>
                                            <td class="label">
                                                Valor Neto
                                            </td>
                                            <td class="input">
                                                <input id="txtValorNeto" style="text-align: right" runat="server" />
                                            </td>
                                            <td>
                                                <table><tr id = "rw_Amortizacion">
                                                <td class="label">Amortizacion</td>
                                                <td id="td_txtAmortizacion" class="input"><input id="txtAmortizacion" name="txtAmortizacion" class="input" runat="server" /></td>
                                                </tr></table>
                                            </td>
                                            <td class="label">
                                                IGV
                                            </td>
                                            <td class="input">
                                                <input id="txtIGV" style="text-align: right" runat="server" />
                                            </td>
                                            <td class="label">
                                                Total
                                            </td>
                                            <td class="input">
                                                <input id="txtTotal" style="text-align: right" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                  </div>
                                    
                                
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr id="rwVencidas">
                                        <td style="text-align: center">
                                            <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="titulo css_lbl_tituloContenido">
                                                        Cuotas Atrasadas
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="jqGrid_lista_A">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div id="jqGrid_pager_A">
                                            </div>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tbLiquidacion" style="text-align: center">
                                            <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="titulo css_lbl_tituloContenido">
                                                        Crédito Vigente
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="jqGrid_lista_B">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div id="jqGrid_pager_B">
                                            </div>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr id="rwConceptos">
                                        <td style="text-align: center">
                                            <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="titulo css_lbl_tituloContenido">
                                                        Otros Conceptos Pendientes
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="jqGrid_lista_C">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div id="jqGrid_pager_C">
                                            </div>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tbComisionesNuevas" style="text-align: center">
                                            <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="titulo css_lbl_tituloContenido">
                                                        Comisiones por Liquidación de Contrato
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="jqGrid_lista_D">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div id="jqGrid_pager_D">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- ******************************* -->
                            <!-- TAB :: Detalle de Comisiones -->
                            <!-- ******************************* -->
                            <div id="tab-1" style="height: 100%;">
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Via de Cobranza</legend>
                                    <table border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                        <tr>
                                            <td class="label">
                                                <input type="hidden" name="hddTipoViaCobranza" id="hddTipoViaCobranza" value="C"
                                                    runat="server" />
                                                <input id="rdbViaCuenta" name="rdbTipoViaCobranza" type="radio" runat="server" checked="true"
                                                    onclick="javacript:fn_setTipoViaCobranza('C');" />
                                                <span>Cuenta </span>
                                            </td>
                                            <td class="label">
                                                <input id="rdbViaVentanilla" name="rdbTipoViaCobranza" type="radio" runat="server"
                                                    onclick="javacript:fn_setTipoViaCobranza('V');" />
                                                <%--<span>Ventanilla </span>--%>
                                            </td>
                                            <td class="label">
                                                <input id="rdbViaCondonacion" name="rdbTipoViaCobranza" type="radio" runat="server"
                                                    onclick="javacript:fn_setTipoViaCobranza('D');" />
                                                <%--<span>Condonacion </span>--%>
                                            </td>
                                            <td class="label">
                                                <input id="rdbViaAdministrativo" name="rdbTipoViaCobranza" type="radio" runat="server"
                                                    onclick="javacript:fn_setTipoViaCobranza('A');" />
                                                <span>Administrativo </span>
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                        <tr id="tb_CargoCuenta">
                                            <td>
                                                <fieldset>
                                                    <legend class="css_lbl_subTitulo">Cargo en cuenta</legend>
                                                    <table border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                                        <tr>
                                                            <td class="label">
                                                                <input type="hidden" name="hddFlagCuentaPropia" id="hddFlagCuentaPropia" runat="server" />
                                                                <input id="rdbCuentaPropia" name="rdbFlagCuentaPropia" type="radio" runat="server"
                                                                    checked="true" onclick="javacript:fn_setFlagCuentaPropia('S');" />
                                                                <span>Cuenta Propia </span>
                                                            </td>
                                                            <td class="label">
                                                                <input id="rdbOtraCuenta" name="rdbFlagCuentaPropia" type="radio" runat="server"
                                                                    onclick="javacript:fn_setFlagCuentaPropia('N');" />
                                                                <span>Cuenta Otra Empresa</span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                                        <tr id="rowCUClienteCargo">
                                                            <td class="label">
                                                                CU Cliente
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtCUClienteCargo" name="txtCUClienteCargo" type="text" class="css_input"
                                                                    runat="server" size="11" />
                                                                <img id="imgBsqClienteRM" alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                                                    vertical-align: middle;" runat="server" onclick="javacript:fn_consultaRM();" />
                                                            </td>
                                                            <td class="input" colspan="3">
                                                                <input id="txtNombreClienteCargo" name="txtNombreClienteCargo" type="text" class="css_input"
                                                                    runat="server" size="40" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Moneda
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbCodMonedaCargo" name="cmbCodMonedaCargo" runat="server">
                                                                    <option value="0">-Seleccionar-</option>
                                                                </select>
                                                            </td>
                                                            <td class="label">
                                                                Tipo Cuenta
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbTipoCuenta" name="cmbTipoCuenta" runat="server">
                                                                    <option value="0">-Seleccionar-</option>
                                                                </select>
                                                            </td>
                                                            <td class="label">
                                                                Nro. Cuenta
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbNroCuenta" name="cmbNroCuenta" runat="server">
                                                                    <option value="0">-Seleccionar-</option>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                    <tr>
                                    </tr>
                                </table>
                            </div>
                            <!-- ****************** -->
                            <!-- TAB :: CRONOGRAMA  -->
                            <!-- ****************** -->
                            <div id="tab-21">
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                    <tr>
                                        <td id="tbCronograma" style="text-align: center">
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
                            <div id="tab-22">
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                    <tr>
                                        <td id="tbCronograma" style="text-align: center">
                                            <table id="jqGrid_lista_F">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div id="jqGrid_pager_F">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- ******************************* -->
                            <!-- TAB :: DOCUMENTOS -->
                            <!-- ******************************* -->
                            <div id="tab-3" style="height: 100%;">
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
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!-- Fin Cuerpo -->
    </div>
    <div style="display: none">
        <asp:Button ID="btnGenerar" runat="server" />
    </div>
    </form>
</body>
</html>
