<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImpuestoVehicularRegistroAgregar.aspx.vb" Inherits="GestionBien_ImpuestoVehicular_frmImpuestoVehicularRegistroAgregar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SGL :: Sistema de Gestión de Leasing</title>
    
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    
    <!-- Estilos --> 
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css" media="all" />
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
    <script type='text/javascript' src="frmImpuestoVehicularRegistroAgregar.aspx.js"> </script>

</head>
<body>
    <form id="frmImpuestoVehicularRegistro" runat="server">
       <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="NUEVO" runat="server" />
    <input type="hidden" id="hddSecFinanciamiento" name="hddSecFinanciamiento" runat="server" />
    <input type="hidden" id="hddCheck" name="hddCheck" runat="server" />
    <input type="hidden" id="hddSecImpuesto" name="hddSecImpuesto" runat="server" />
    <input type="hidden" id="hddOrigen" name="hddOrigen" runat="server" />
    <input type="hidden" id="hddEstadoCobro" name="hddEstadoCobro" runat="server" />
    <input type="hidden" id="hddEstadoPago" name="hddEstadoPago" runat="server" />
    <input type="hidden" id="hddNroCuotas" name="hddNroCuotas" runat="server" />
    <input type="hidden" id="hddFechaTransferencia" name="hddFechaTransferencia" runat="server" />
    <input type="hidden" id="hddCheque" name="hddCheque" runat="server" />
    <input type="hidden" id="hddVer" name="hddVer" runat="server" />
    <input type="hidden" id="hddNroLote" name="hddNroLote" runat="server" />
    <input type="hidden" id="hddCodigoUnico" name="hddCodigoUnico" value="NUEVO" runat="server" />
    <%--Inicio IBK - AAE - Variable para ver si tengo el nro del lote asignado --%>    
    <input type="hidden" id="hidReadOnly" name="hidReadOnly" value="" runat="server" />    
    <input id="hidTengoLote" type="hidden" runat="server" />
    <input type="hidden" name="hddPerfil" id="hddPerfil" value="" runat="server" />
    <%--Fin IBK - AAE--%>
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_impuesto.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">Gestión del Bien</div>
                    <div class="css_lbl_titulo">Impuesto Vehicular :: Registro</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones" style="height:60px;vertical-align:top;">
                    
                    <div class="dv_img_boton" id="dv_img_boton" style="border: 0">
						 <a href="javascript:parent.fn_util_CierraModal();">
                           <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    
                    <div id="dv_guardar" class="dv_img_boton">
                        <a href="javascript:fn_grabarInpuestoVehicular()">
                            <img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
                    </div>
                    
			        <div id="dv_separador" class="dv_img_boton_separador">
						:
					</div>
                		        
					<div id="dv_documentos" class="dv_img_boton" style="width:80px;">
				        <a href="javascript:fn_GBAbreDocumentos();">
					        <img alt="" src="../../Util/images/ico_version.gif" border="0"/><br />
					        Documentos
				        </a>
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
        </table>
        <br />
        <div>
            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="label">
                                    Nº Contrato
                                </td>
                                <td class="input">
                                    <input id="txtNroContrato" name="txtNroContrato" type="text" class="css_input_inactivo" disabled="True" runat="server"  />
                                </td>
                                <td class="label">
                                   Placa 
                                </td>
                                <td class="input">
                                   <input id="txtPlaca" name="txtPlaca" type="text" class="css_input_inactivo" disabled="True" runat="server" /> 
                                </td>
                                <td class="label">
                                   Descripción del Bien 
                                </td>
                                <td class="input">
                                    <textarea id="txtDescripcionInmueble" name="txtDescripcionInmueble" rows="2" cols="30"  disabled="True" class="css_input_inactivo"
                                     runat="server"></textarea>
                                </td> 
                                 
                            </tr>
                            <tr>
                                <td class="label">
                                    <%--Inicio IBK - AAE - Se cambia la fecha de declaración por fecha de registro --%>    
                                    <%--F. Declaración--%>
                                    F. Registros Públicos
                                </td>
                                <td class="input">
                                    <input id="txtFechaDeclaracion" name="txtFechaDeclaracion" type="text" class="css_input"  runat="server"  />
                                    <input id="txtFechaRRPP" name="txtFechaRRPP" type="text" class="css_input"  runat="server"  />
                                </td>
                                <td class="label">
                                    F. Reg. Municipal
                                </td>
                                <td class="input">
                                    <input id="txtFechaMuni" name="txtFechaMuni" type="text" class="css_input"  runat="server"  />
                                </td>
                                <td/>
                                <td/>
                            </tr>
                            <tr> 
                                <td class="label">
                                    Periodo
                                </td>
                                <td class="input">
                                    <input id="txtPeriodo" name="txtPeriodo" type="text" class="css_input"  runat="server"  />
                                </td>
                                <td/>
                                <td/>
                                <td class="label">
                                    Importe
                                </td>
                                <td class="input">
                                    <input id="txtImporte" name="txtImporte" type="text" class="css_input"  runat="server"  />
                                </td>  
                            </tr>
                            <tr>
                                 <td class="label">
                                    Moneda
                                </td>
                                <td class="input">
                                    <select id="ddlMoneda" runat="server">
                                        <option value="0">- Seleccionar -</option>
                                    </select>
                                </td>
                                
                                <td class="label">
                                    Nº Cuota
                                </td>
                                <td class="input">
                                    <select id="ddlCuota" runat="server">
                                        <option value="0">- Seleccionar -</option>
                                    </select>
                                </td>
                                <td class="label">
                                    Pago Cliente
                                </td>
                                <td class="input">
                                    <input id="cbPagoCliente" type="checkbox" runat="server" />
                                </td>
                                 
                            </tr>
                            <tr>
                                <td class="label">
                                    F. Pago
                                </td>
                                <td class="input">
                                    <input id="txtFechaPago" name="txtFechaPago" type="text" class="css_input"  runat="server"  />
                                </td> 
                                <td class="label">
                                    Estado Pago
                                </td>
                                <td class="input">
                                     <select id="ddlEstadoPago" runat="server">
                                        <option value="0">- Seleccionar -</option>
                                    </select>
                                </td>
                                <td class="label">
                                    F. Cobro
                                </td>
                                <td class="input">
                                    <input id="txtFechaCobro" name="txtFechaCobro" type="text" class="css_input_inactivo"  runat="server"  />
                                </td>
                                 
                            </tr>
                            <tr>
                                <td class="label">
                                    Estado Cobro
                                </td>
                                <td class="input">
                                    <select id="ddlEstadoCobro" runat="server">
                                        <option value="0">- Seleccionar -</option>
                                    </select>
                                </td>
                                <%-- Inicio IBK - AAE - Variable para ver si tengo el nro del lote asignado --%>
                                <td class="label">
                                    Cobro Adelantado
                                </td>
                                <td class="input">
                                    <input id="cbCobroAdelantado" type="checkbox" runat="server" />
                                </td>                                 
                                <td class="label">
                                    No Aplicar Comisión
                                </td>
                                <td class="input">
                                    <input id="cbNoComision" type="checkbox" runat="server" />
                                </td>                                 
                                <%-- Fin IBK --%>
                            </tr>
                            <tr>
                                <td class="label">
                                                Observaciones
                                            </td>
                                            <td class="input" colspan="6">
                                                <textarea id="txtObservaciones" name="txtObservaciones" runat="server"
                                                    cols="77" type="text" rows="2" />
                                            </td>
                            </tr>
                        </table>
        </div>
                   
        
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
