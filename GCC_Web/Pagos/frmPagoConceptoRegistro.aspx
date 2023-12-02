<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPagoConceptoRegistro.aspx.vb"
    Inherits="Pagos_frmPagoConceptoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
        
    <!-- Estilos --> 
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css" media="all" />
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
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmPagoConceptoRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmPagoConceptoRegistro" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos" style="width: 237px">
                    <div class="css_lbl_subTitulo">
                        Pagos</div>
                    <div class="css_lbl_titulo">
                        Pago de Concepto ::
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
                    <div id="dv_botonEjecutar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_ejecutarPagoConcepto(1);" title="Ejecutar Pago">
                            <img alt="" src="../Util/images/ico_acc_ejecutarID.gif" width="35px" /><br />
                            Ejecutar </a>
                    </div>
                    <div id="dv_botonAnular" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_anularPagoConcepto();" title="Anular">
                            <img alt="" src="../Util/images/ico_acc_devolverID.gif" width="35px" /><br />
                            Anular </a>
                    </div>
                    <div id="dv_botonExtornar" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_extornarPagoConcepto();" title="Extornar">
                            <img alt="" src="../Util/images/ico_acc_devolverID.gif" width="35px" /><br />
                            Extornar </a>
                    </div>
                    <div id="dv_botonEditar" class="dv_img_boton">
                        <a href="javascript:fn_editarPagoConcepto();">
                            <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" /><br />
                            Editar </a>
                    </div>
                    <div id="dv_botonGrabar" class="dv_img_boton">
					    <a href="javascript:fn_ejecutarPagoConcepto(0);">
						    <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0"/><br />
						    Guardar
					    </a>
				    </div>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="" runat="server" />
        <input type="hidden" name="hddError" id="hddError" value="" runat="server" />
        <input type="hidden" name="hddActivacion" id="hddActivacion" value="" runat="server" />
                        
        <input type="hidden" name="hddCodSolicitudCredito" id="hddCodSolicitudCredito" value="" runat="server" />
        <input type="hidden" name="hddNumSecRecuperacion" id="hddNumSecRecuperacion" value="" runat="server" />
        <input type="hidden" name="hddEstadoRecuperacion" id="hddEstadoRecuperacion" value="" runat="server" />
        <input type="hidden" name="hddCodMonedaContrato" id="hddCodMonedaContrato" value="" runat="server" />
        
        <input type="hidden" name="hddPerfilUsuario" id="hddPerfilUsuario" value="" runat="server" />

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
                                    Datos del Contrato
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input" runat="server" />
                                        <img id="imgBsqContrato" alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                            vertical-align: middle;" runat="server" />
                                        <input type="hidden" id="HidCodigoUnico" value="0" runat="server" />
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
                                        <input id="txtTipoPersona" name="txtTipoPersona" type="text" class="css_input" value=""
                                            runat="server" />
                                    </td>
                                    <td class="label">
                                        Tipo Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtTipoDocumento" name="txtTipoDocumento" type="text" class="css_input"
                                            value="" runat="server" />
                                    </td>
                                    <td class="label">
                                        N° Documento
                                    </td>
                                    <td class="input" colspan="3">
                                        <input id="txtNumeroDocumento" name="txtNumeroDocumento" type="text" class="css_input"
                                            value="" size="30" runat="server" />
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
                                        Ejecutivo Leasing
                                    </td>
                                    <td class="input">
                                        <input id="txtEjecutivoLeasing" name="txtEjecutivoLeasing" type="text" class="css_input"
                                            value="" size="30" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Estado Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoContrato" name="txtEstadoContrato" type="text" class="css_input" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Pago
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datosPago" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Fecha Valor
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaValor" name="txtFechaValor" runat="server" />
                                    </td>
                                    
                                    <td class="label">
                                        Correlativo de Pago
                                    </td>
                                    <td class="input">
                                        <input id="txtNumSecRecuperacion" name="txtNumSecRecuperacion" runat="server" />
                                    </td>
                                    <td class="label">
                                        Nro. Autorización
                                    </td>
                                    <td class="input">
                                        <input id="txtCodAutorizacionRecuperacion" name="txtCodAutorizacionRecuperacion" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Concepto
                                    </td>
                                    <td class="input" colspan = "3">
                                        <select id="cmbConcepto" name="cmbConcepto" style="width: 400px" runat="server">
                                            <option value="0">-seleccionar-</option>
                                        </select>
                                    </td>
                                    
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <select id="cmbCodMoneda" name="cmbCodMoneda" runat="server">
                                            <option value="0">-seleccionar-</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Glosa Comprobante
                                    </td>
                                    <td class="input" colspan="7">
                                        <textarea id="txtGlosaConceptoComprobante" class="css_input" rows="3" cols="175" runat="server"> </textarea>
                                    </td>
                                </tr>
                                <tr>
                                                <td class="label">
                                                    Reembolso
                                                </td>
                                                <td class="input">
                                                    <input id="txtMontoReembolso" name="txtMontoReembolso" style="text-align: right" runat="server" />
                                                </td>
                                                <td class="label">
                                                    IGV Reembolso
                                                </td>
                                                <td class="input">
                                                    <input id="txtMontoIGVReembolso" name="txtMontoIGVReembolso" style="text-align: right" runat="server" />
                                                </td>
                                                <td class="label">
                                                    Comisión
                                                </td>
                                                <td class="input">
                                                    <input id="txtMontoComision" name="txtMontoComision" style="text-align: right" runat="server" />
                                                </td>
                                                <td class="label">
                                                    IGV Comision
                                                </td>
                                                <td class="input">
                                                    <input id="txtMontoIGV" name="txtMontoIGV" style="text-align: right" runat="server" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="label">
                                                    Reajuste Reembolso
                                                </td>
                                                <td class="input">
                                                    <input id="txtReajusteReembolso" value="0.00" name="txtReajusteReembolso" runat="server" />
                                                </td>
                                                <td class="label">
                                                    Reajuste IGV Reembolso
                                                </td>
                                                <td class="input">
                                                    <input id="txtReajusteIGVReembolso" value="0.00" name="txtReajusteIGVReembolso" runat="server" />
                                                </td>
                                                <td class="label">
                                                    Reajuste Comisión
                                                </td>
                                                <td class="input">
                                                    <input id="txtReajusteComision" value="0.00" name="txtReajusteComision" runat="server" />
                                                </td>
                                                <td class="label">
                                                    Reajuste IGV Comision
                                                </td>
                                                <td class="input">
                                                    <input id="txtReajusteIGV" value="0.00" name="txtReajusteIGVComision" runat="server" />
                                                </td>
                                            </tr>
                                
                                <tr>


                                    <td class="label">
                                        Monto del Pago
                                    </td>
                                    <td class="input">
                                        <input id="txtMontoPago" name="txtMontoPago" runat="server" />
                                    </td>
                                </tr>
                            </table>
                                        
                        </div>
                        <br />

                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Via de Cobranza
                                </td>
                            </tr>
                        </table>
                        <div id="dv_viaCobranza" class="dv_tabla_contenedora">
                            <table id="tb_formulario" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                    
                                        <table border="0" cellpadding="0" cellspacing="3" style="width: 100%;">
                                            <tr>
                                                <td class="label">
                                                    <input type="hidden" name="hddTipoViaCobranza" id="hddTipoViaCobranza" value="C"
                                                        runat="server" />
                                                    <input id="rdbViaCuenta" name="rdbTipoViaCobranza" type="radio" runat="server" checked="true" onclick="javacript:fn_setTipoViaCobranza('C');" />
                                                    <span>Cuenta </span>
                                                </td>
                                                <td class="label">
                                                    <input id="rdbViaVentanilla" name="rdbTipoViaCobranza" type="radio" runat="server" onclick="javacript:fn_setTipoViaCobranza('V');" />
                                                    <span>Ventanilla </span>
                                                </td>
                                                <td class="label">
                                                    <input id="rdbViaCondonacion" name="rdbTipoViaCobranza" type="radio" runat="server" onclick="javacript:fn_setTipoViaCobranza('D');" />
                                                    <span>Condonacion </span>
                                                </td>
                                                <td class="label">
                                                    <input id="rdbViaAdministrativo" name="rdbTipoViaCobranza" type="radio" runat="server" onclick="javacript:fn_setTipoViaCobranza('A');" />
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
                                                            
                                                            <input id="rdbCuentaPropia" name="rdbFlagCuentaPropia" type="radio" runat="server" checked = "true" onclick="javacript:fn_setFlagCuentaPropia('S');" />
                                                            <span>Cuenta Propia </span>
                                                        </td>
                                                        <td class="label">
                                                            <input id="rdbOtraCuenta" name="rdbFlagCuentaPropia" type="radio" runat="server" onclick="javacript:fn_setFlagCuentaPropia('N');" />
                                                            <span>Cuenta Otra Empresa</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table border="0" cellpadding="0" cellspacing="3" style="width: 100%;" >
                                                <tr id = "rowCUClienteCargo" >
                                                    <td class="label">
                                                        CU Cliente
                                                    </td>
                                                    <td class="input">
                                                        <input id="txtCUClienteCargo" name="txtCUClienteCargo" type="text" class="css_input" runat="server" size="11" />
                                                        <img id="imgBsqClienteRM" alt="" src="../Util/images/ico_buscar.jpg" style="cursor:pointer; vertical-align:middle;" runat="server" onclick="javacript:fn_consultaRM();" />
                                                    </td>
                                                    <td class="input" colspan="3">
                                                        <input id="txtNombreClienteCargo" name="txtNombreClienteCargo" type="text" class="css_input" runat="server" size = "40"/>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="label" >
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
                                    </td>
                                </tr>
                             </table>   
                        </div>
                        <br />

                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Estado del Pago
                                </td>
                            </tr>
                        </table>
                        <div id="dv_Concepto" class="dv_tabla_contenedora">
                            
                               <table id="tb_formulario" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="label">
                                        Fecha Proceso
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaRecuperacion" name="txtFechaRecuperacion" class="css_input_inactivo" runat="server" />
                                    </td>
                                    
                                    <td class="label">
                                        Estado Operación
                                    </td>
                                    <td class="input">
                                        <select id="cmbEstadoRecuperacion" name="cmbEstadoRecuperacion" runat="server">
                                            <option value="0">- Seleccionar -</option>
                                        </select>
                                    </td>
                                 </tr>
                                    <tr id="rw_MotivoAnulacionExtorno">
                                        <td class="label">
                                            Motivo
                                        </td>
                                        <td class="input" colspan="3">
                                            <textarea id="txtMotivo" class="css_input" rows="3" cols="80" disabled="disabled" runat="server"> </textarea>
                                        </td>
                                    </tr>
                                </table>
                        </div>
                        <br />
                        
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Identificación del Pago por Ventanilla
                                </td>
                            </tr>
                        </table>
                        <div id="dv_IdVentanilla" class="dv_tabla_contenedora">
                            
                               <table id="tb_formulario" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td class="label">
                                            Id. Transactor
                                        </td>
                                        <td class="input">
                                            <input id="txtCodOperacionGINA" name="txtCodOperacionGINA" runat="server" />
                                        </td>
                                        <td class="label">
                                            Fecha del Pago
                                        </td>
                                        <td class="input">
                                            <input id="txtFechaProcesoPago" name="txtFechaProcesoPago" class="css_input_inactivo" runat="server" />
                                        </td>
                                        <td class="label">
                                            Terminal
                                        </td>
                                        <td class="input">
                                            <input id="txtCodTerminalPago" name="txtCodTerminalPago" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Tienda de Pago
                                        </td>
                                        <td class="input">
                                            <select id="cmbTiendaPago" name="cmbTiendaPago" runat="server">
                                                <option value="0">-Seleccionar-</option>
                                            </select>
                                        </td>
                                        <td class="label">
                                            Usuario
                                        </td>
                                        <td class="input">
                                            <input id="txtCodUsuarioPago" name="txtCodUsuarioPago" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            1er Medio de Pago
                                        </td>
                                        <td class="input">
                                            <input id="txtCodModoPago" name="txtCodModoPago" runat="server" />
                                        </td>
                                        <td class="label">
                                            2do Medio de Pago
                                        </td>
                                        <td class="input">
                                            <input id="txtCodModoPago2" name="txtCodModoPago2" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                        </div>
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
