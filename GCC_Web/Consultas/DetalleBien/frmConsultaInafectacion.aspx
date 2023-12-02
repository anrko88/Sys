<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaInafectacion.aspx.vb"
    Inherits="Consultas_frmConsultaInafectacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documentos y Comentarios</title>
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

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>

    <script type="text/javascript" src="../../Util/js/jquery/jquery.dateFormat-1.0.js"></script>

    <script type="text/javascript" src="../../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.modal.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.date.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"> </script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmConsultaInafectacion.aspx.js"></script>

</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_mantenimiento.jpg" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consultas</div>
                    <div class="css_lbl_titulo">
                        Inafectación</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    
                </td>
            </tr>
        </table>
        <input type="hidden" name="hddCodigoContrato" id="hddCodigoContrato" value=""
            runat="server" />
        <input type="hidden" name="hddCodigoBien" id="hddCodigoBien" value="" runat="server" />
        <input type="hidden" name="hddEstado" id="hddEstado" value="" runat="server" />
        <input type="hidden" name="hddAnioFabricacion" id="hddAnioFabricacion" value="" runat="server" />
        
        <input type="hidden" name="hddCodigoInafectacion" id="hddCodigoInafectacion" value="" runat="server" />
        <input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="NUEVO"
            runat="server" />
        <asp:Button ID="btnGrabar" runat="server" Style="display: none;" Text="Graba" />
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
	    </table>
	    <br/>
        <table id="tb_formulario" border="0" cellpadding="0">
            <tr>
                <td class="label">
                    Periodo
                </td>
                <td class="input" id="txtPeriodo" runat="server" >
                  <%-- <input id="txtPeriodo" type="text" class="css_input" runat="server" />--%>
                </td>
                <td class="label">
                    Fecha Envío de Carta
                </td>
                <td class="input" id="txtFecEnvioCarta" runat="server" >
                    <%--<input id="txtFecEnvioCarta" type="text" class="css_input" runat="server" />--%>
                </td>
            </tr>
            <tr>
               <td class="label">
                    Fecha Recepción Documentos
                </td>
                <td class="input" id="txtFecRecepDocumentos" runat="server">
                    <%--<input id="txtFecRecepDocumentos" type="text" class="css_input" runat="server" />--%>
                </td>
                <td class="label">
                    Fecha Presentación a SAT
                </td>
                <td class="input" id="txtFecPresentacionSat" runat="server" >
                    <%--<input id="txtFecPresentacionSat" type="text" class="css_input" runat="server" />--%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Fecha Notificación
                </td>
                <td class="input" id="txtFecNotificacion"  runat="server">
                  <%--  <input id="txtFecNotificacion" type="text" class="css_input" runat="server" onkeyup="javascript:fn_ValidarFechaNotificacion();" />--%>
                </td>
                <td class="label">
                    Nro. Resolución
                </td>
                <td class="input" id="txtNroResolucion" runat="server" >
                    <%--<input id="txtNroResolucion" type="text" class="css_input" runat="server" />--%>
                </td>
                
            </tr>
            <tr>
                <td class="label">
                    Estado
                </td>
                <td class="input" id="ddlEstado" runat="server" >
                <%-- <select id="ddlEstado" runat="server" name="ddlEstado">
                        <option value='0'>[-Seleccione-]</option>
                    </select>--%>
                </td>
            </tr>
          
        </table>
    </div>
    </form>
</body>
</html>
