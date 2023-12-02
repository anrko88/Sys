<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaImpuestoVehicularRegistroAgregar.aspx.vb" Inherits="Consultas_ImpuestoVehicular_frmConsultaImpuestoVehicularRegistroAgregar" %>

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
    <script type='text/javascript' src="frmConsultaImpuestoVehicularRegistroAgregar.aspx.js"> </script>

</head>
<body>
    <form id="frmConsultaImpuestoVehicularRegistroAgregar" runat="server">
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
    <input type="hidden" id="hddCodigoUnico" name="hddCodigoUnico" value="NUEVO" runat="server" />
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_impuesto.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">Consultas</div>
                    <div class="css_lbl_titulo">Impuesto Vehicular :: Consulta</div>
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
                                <td class="input" id="TDNumeroContrato" runat="server">
                                </td>
                                
                                <td class="label">
                                   Placa 
                                </td>
                                <td class="input" id="TDPlaca" runat="server">
                                </td>
                                <td class="label">
                                   Descripción del Bien 
                                </td>
                                <td class="input" id="TDDescripcionBien" runat="server">
                                </td> 

                            </tr>
                            <tr>
                                <td class="label">
                                    F. Declaración
                                </td>
                                <td class="input" id="TDFechaDeclaracion" runat="server" >
                                   
                                </td>
                                <td class="label">
                                    Periodo
                                </td>
                                <td class="input" id="TDPeriodo" runat="server">
                                </td>
                                <td class="label">
                                    Importe
                                </td>
                                <td class="input" id="TDImporte" runat="server">
                                  
                                </td>  
                            </tr>
                            <tr>
                                 <td class="label">
                                    Moneda
                                </td>
                                <td class="input" id="TDMoneda" runat="server">
                                </td>
                                
                                <td class="label">
                                    Nº Cuota
                                </td>
                                <td class="input" id="TDCuota" runat="server">
                                </td>
                                <td class="label">
                                    Pago Cliente
                                </td>
                                <td class="input" >
                                    <input id="cbPagoCliente" type="checkbox" runat="server" disabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    F. Pago
                                </td>
                                <td class="input" id="TDFechaPago" runat="server">
                                  </td> 
                                <td class="label">
                                    Estado Pago
                                </td>
                                <td class="input" id="TDEstadoPago" runat="server">
                                    
                                </td>
                                <td class="label">
                                    F. Cobro
                                </td>
                                <td class="input" id="TDFechaCobro" runat="server">
                                </td>
                                 
                            </tr>
                            <tr>
                                <td class="label">
                                    Estado Cobro
                                </td>
                                <td class="input" id="TDEstadoCobro" runat="server">

                                </td> 
                                
                            </tr>
                            <tr>
                                <td class="label">
                                                Observaciones
                                            </td>
                                            <td class="input" colspan="6" id="TDObservaciones" runat="server">
                                            </td>
                            </tr>
                        </table>
        </div>
                   
        
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
