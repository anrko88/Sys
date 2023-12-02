<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGenerarCarta.aspx.vb" Inherits="Pagos_frmGenerarCarta" %>

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


    <script src="frmGenerarCarta.aspx.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmGenerarCarta" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_cotizacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos" style="width: 237px">
                    <div class="css_lbl_subTitulo">
                        Reportes</div>
                    <div class="css_lbl_titulo">
                        Generación de Cartas
                        <label id="lblOperacion" runat="server">
                        </label>
                    </div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_botonCancelar" class="dv_img_boton">
                        <a href="javascript:javascript:parent.fn_util_CierraModal();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    
                    <div id="dv_botonGrabar" class="dv_img_boton">
					    <a href="javascript:fn_VerReporte();">
						    <img alt="" src="../Util/images/ico_acc_importar.gif" border="0"/><br />
						    Ver Reporte
					    </a>
				    </div>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hddCartas" name="hddCartas" value="" runat="server" />

        
      
  <div id="dv_contenedor" class="css_scrollPane">
      
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                   <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                     
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nº Lote
                                    </td>
                                    <td class="input">
                                       <input id="txtNroLote" type="text" class="css_input" runat="server" />
                                        <img id="imgBsqLote" alt="" src="../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                                vertical-align: middle;" runat="server" onclick="javascript:fn_CargaData();" />
                                    </td>
                                    <td class="label">
                                        Municipalidad
                                    </td>
                                    <td class="input">
                                        <input id="txtMunicipalidad" type="text" class="css_input_inactivo" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Concepto
                                    </td>
                                    <td class="input">
                                        <input id="txtConcepto" type="text" class="css_input_inactivo" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    <td class="label">
                                        Importe Total
                                    </td>
                                    <td class="input">
                                        <input id="txtImporteTotal" type="text" class="css_input_inactivo" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="label">
                                      Periodo
                                    </td>
                                    <td class="input">
                                        <input id="txtPeriodo" type="text" class="css_input_inactivo" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    <td class="label">
                                        Fecha de Generación
                                    </td>
                                    <td class="input">
                                        <input id="txtGeneracion" type="text" class="css_input_inactivo" disabled="disabled" readonly="readonly" runat="server" />
                                    </td>
                                    
                                </tr>
                                
                            </table>
                        </div>
                      
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
     <div style="display: none">
        <asp:Button ID="btnGenerar" runat="server" />
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
