<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImpuestoVehicularVerificarBienes.aspx.vb" Inherits="GestionBien_ImpuestoVehicular_frmImpuestoVehicularVerificarBienes" %>

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

    <script type='text/javascript' src="frmImpuestoVehicularVerificarBienes.aspx.js"> </script>

</head>
<body>
    <form id="frmVerificarBienes" runat="server">
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
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Impuesto Vehicular : Verificar Bienes</div>
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
                    <div id="Div1" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_GuardarBienes();">
                            <img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Grabar </a>
                    </div>
                </td>
            </tr>
        </table>
        <input type="hidden" name="hidNumeroContrato" id="hidNumeroContrato" value="" runat="server" />
        <input type="hidden" name="hidSecFinanciamiento" id="hidSecFinanciamiento" value=""
            runat="server" />
        <input type="hidden" name="hidCodClasificacion" id="hidCodClasificacion" value=""
            runat="server" />
        <input type="hidden" name="hidCodTipoBien" id="hidCodTipoBien" value="" runat="server" />
        <input type="hidden" name="hidMensaje" id="hidMensaje" value="" runat="server" />
        <input type="hidden" name="hidNroSerie" id="hidNroSerie" value="" runat="server" />
        <asp:Button ID="btnGrabar" runat="server" Style="display: none;" Text="Graba" />
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            padding-right: 0px;">
            <tr>
                <td class="lineas">
                </td>
            </tr>
        </table>
        <br />
       <!-- VEHICULOS -->
        <%--<div id="dv_contenedor_Vehiculo" class="css_scrollPane">--%>
        <div id="dv_datos_vehiculo">
            <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
                <colgroup>
                    <col style="width: 10%;" />
                    <col style="width: 20.33%;" />
                    <col style="width: 15%;" />
                    <col style="width: 20.33%;" />
                    <col style="width: 12%;" />
                    <col style="width: 22.33%;" />
                </colgroup>
                <tr>
                    <tr>
                    <td class="label">
                        Departamento
                    </td>
                    <td class="input">
                        <select id="ddlDepartamentoVehiculo" name="ddlDepartamentoVehiculo" runat="server"
                            onchange="javascript:fn_cargaComboProvinciaVehiculo(this.value);">
                        </select>
                        <input type="hidden" name="hidDepartamentoVehiculo" id="hidDepartamentoVehiculo"
                            value="" runat="server" />
                    </td>
                    <td class="label">
                        Provincia
                    </td>
                    <td class="input">
                        <select id="ddlProvinciaVehiculo" name="ddlProvinciaVehiculo" onchange="javascript:fn_cargaComboDistritoVehiculo(ddlDepartamentoVehiculo.value,this.value);">
                            <option value="0">[- Seleccionar -]</option>
                        </select>
                        <input type="hidden" name="hidProvinciaVehiculo" id="hidProvinciaVehiculo" value=""
                            runat="server" />
                    </td>
                    <td class="label">
                        Distrito
                    </td>
                    <td class="input">
                        <select id="ddlDistritoVehiculo" name="ddlDistritoVehiculo">
                            <option value="0">[- Seleccionar -]</option>
                        </select>
                        <input type="hidden" name="hidDistritoVehiculo" id="hidDistritoVehiculo" value=""
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Ubicación
                    </td>
                    <td class="input">
                        <input id="txtUbicacionVehiculo" name="txtUbicacionVehiculo" type="text" class="css_input"
                            runat="server" />
                    </td>
                    <td class="label">
                        Uso
                    </td>
                    <td class="input">
                        <input id="txtUsoVehiculo" name="txtUsoVehiculo" 
                            type="text" class="css_input" runat="server" />
                    </td>
                    
                </tr>
                <tr>
                    <td class="label">
                        Nº Serie
                    </td>
                    <td class="input">
                        <input id="txtSerieVehiculo" name="txtSerieVehiculo" type="text" class="css_input"
                            runat="server" />
                    </td>
                    <td class="label">
                        Nº Motor
                    </td>
                    <td class="input">
                        <input id="txtMotorVehiculo" name="txtMotorVehiculo" type="text" class="css_input"
                            runat="server" />
                    </td>
                    <td class="label">
                        Año
                    </td>
                    <td class="input">
                        <input id="txtAnioVehiculo" name="txtAnioVehivulo" type="text" class="css_input"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Marca
                    </td>
                    <td class="input">
                        <input id="txtMarcaVehiculo" name="txtMarcaVehivulo" type="text" class="css_input"
                            runat="server" />
                    </td>
                    <td class="label">
                        Modelo
                    </td>
                    <td class="input">
                        <input id="txtModeloVehiculo" name="txtModeloVehiculo" type="text" class="css_input"
                            runat="server" />
                    </td>
                    <td class="label">
                        Tipo Carrocería
                    </td>
                    <td class="input">
                        <input id="txtCarroceriaVehiculo" name="txtCarroceriaVehiculo" type="text" class="css_input"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Descripción
                    </td>
                    <td class="input">
                        <textarea id="txtDescripcionVehiculo" runat="server" name="txtDescripcionVehiculo"
                            class="css_input" rows="2" cols="30"></textarea>
                    </td>
                    <td class="label">
                        Estado del Bien
                    </td>
                    <td class="input">
                        <select id="ddlEstadoBienVehiculo" name="ddlEstadoBienVehiculo" runat="server">
                        </select>
                        <input type="hidden" name="hidEstadoBienVehiculo" id="hidEstadoBienVehiculo" value=""
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Cantidad
                    </td>
                    <td class="input">
                        <input id="txtCantidadVehiculo" name="txtCantidadVehiculo" type="text" class="css_input"
                            runat="server" />
                    </td>
                    <td class="label">
                        Medidas
                    </td>
                    <td class="input">
                        <input id="txtMedidasVehiculo" name="txtMedidasVehiculo" type="text" class="css_input"
                            runat="server" />
                    </td>
                    <td class="label">
                        Placa
                    </td>
                    <td class="input">
                        <input id="txtPlacaVehiculo" name="txtPlacaVehiculo" type="text" class="css_input"
                            runat="server" />
                    </td>
                </tr>
                
                
            </table>
        </div>
   
    </div>
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
    </form>
</body>
</html>
