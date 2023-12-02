<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBienEditar.aspx.vb" Inherits="Desembolso_frmBienEditar" %>

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

    <script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.ui.global.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.validText.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.validNumber.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.maxlength.js"> </script>

    <script type="text/javascript" src="../Util/js/jquery/json2.js"></script>

    <script type="text/javascript" src="../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.modal.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.funcion.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.date.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.ajax.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script src="frmBienEditar.aspx.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmBienEditar" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Desembolso</div>
                    <div class="css_lbl_titulo">
                        Desembolso :: Modificación Bien</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal2();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="Div1" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_grabar();">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
                    </div>
                </td>
            </tr>
        </table>
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="lineas">
                </td>
            </tr>
        </table>
        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titulo css_lbl_tituloContenido">
                    <div id="dvContrato">
                    </div>
                    <div id="dvRegistro">
                        Registro</div>
                </td>
            </tr>
        </table>
        <!-- *********************************************************************
             Mantenimiento de Bienes
             *********************************************************************
        -->
        <div id="dv_datos_inmueble" class="dv_tabla_contenedora">
            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                <tr>
                    <td class="label">
                        Cantidad
                    </td>
                    <td class="input">
                        <input id="txtCantidad" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td class="label">
                        Descripción
                    </td>
                    <td class="input" colspan="3">
                        <textarea id="txtDescripcionDemanda" runat="server" class="css_input_inactivo"
                            cols="50" rows="2" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Uso
                    </td>
                    <td class="input">
                        <input id="txtUso" type="text" class="css_input_inactivo" runat="server" value="" />
                    </td>
                    <td class="label">
                        Ubicación
                    </td>
                    <td class="input" colspan="3">
                        <input id="txtUbicacion" type="text" class="css_input_inactivo" runat="server" style="width: 400px"
                            value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Departamento
                    </td>
                    <td class="input">
                        <select id="ddlDepartamento" runat="server" onchange="javascript:fn_cargaComboProvinciaBien(this.value);">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodDepartamento" runat="server" value="" />
                    </td>
                    <td class="label">
                        Provincia
                    </td>
                    <td class="input">
                        <select id="ddlProvincia" runat="server" onchange="javascript:fn_cargaComboDistritoBien(ddlDepartamento1.value,this.value);">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodProvincia" runat="server" value="" />
                    </td>
                    <td class="label">
                        Distrito
                    </td>
                    <td class="input">
                        <select id="ddlDistrito" runat="server">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodDistrito" runat="server" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Clasificación del Bien
                    </td>
                    <td class="input">
                        <input id="txtClasificacionBien" type="text" class="css_input_inactivo" runat="server"
                            value="" style="width: 150px" />
                    </td>
                    <td class="label">
                        Tipo de Bien
                    </td>
                    <td class="input">
                        <select id="cmbTipoBien" name="cmbTipoBien" onblur="fWidthCombo(0);" runat="server"
                            style="width: 100px">
                            <option selected="selected">[-Seleccione-]</option>
                        </select>
                    </td>
                    <td class="label">
                        Estado del Bien
                    </td>
                    <td class="input">
                        <input id="txtEstadoBien" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Moneda
                    </td>
                    <td class="input">
                        <select id="ddlMonedaBien" name="D1" runat="server">
                            <option selected="selected">- Seleccione -</option>
                        </select>
                    </td>
                    <td class="label">
                        Valor del Bien
                    </td>
                    <td class="input">
                        <input id="txtValorBien" type="text" style="text-align: right;" runat="server" value="" />
                    </td>
                    <td class="input">
                       <%-- &nbsp; Color--%>
                    </td>
                    <td class="input">
<%--                        &nbsp;
                        <input id="txtColor" type="text" runat="server" class="css_input" maxlength="20"
                            value="" />--%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Partida Registral
                    </td>
                    <td class="input">
                        <input id="txtPartidaRegistral" type="text" runat="server" value="" />
                    </td>
                    <td class="label">
                        Oficina Registral
                    </td>
                    <td class="input">
                        <input id="txtOficinaRegistral" type="text" runat="server" value="" />
                    </td>
                    <td class="label">
                        Fecha de Transferencia
                    </td>
                    <td class="input">
                        <input id="txtFechaTransferencia" maxlength="10" type="text" class="css_input" runat="server"
                            value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Observaciones
                    </td>
                    <td class="input" colspan="5">
                        <textarea id="txtObservaciones" cols="20" runat="server" runat="server" style="width: 99%;
                            height: 43px;"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <!-- *********************************************************************
             Mantenimiento de Vehículo
             *********************************************************************
        -->
        <div id="dv_datos_vehiculo" class="dv_tabla_contenedora">
            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                <tr>
                    <td class="label">
                        Cantidad
                    </td>
                    <td class="input">
                        <input id="txtCantidad1" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td class="label">
                        Descripción
                    </td>
                    <td class="input" colspan="3">
                        <textarea id="txtDescripcionBien1"  runat="server" class="css_input_inactivo"
                            cols="50" rows="2" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Uso
                    </td>
                    <td class="input">
                        <input id="txtUso1" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td class="label">
                        Ubicación
                    </td>
                    <td class="input" colspan="3">
                        <input id="txtUbicacion1" type="text" runat="server" class="css_input_inactivo" value=""
                            style="width: 400px" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Departamento
                    </td>
                    <td class="input">
                        <select id="ddlDepartamento1" runat="server" onchange="javascript:fn_cargaComboProvincia(this.value);">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodDepartamento1" runat="server" value="" />
                    </td>
                    <td class="label">
                        Provincia
                    </td>
                    <td class="input">
                        <select id="ddlProvincia1" runat="server" onchange="javascript:fn_cargaComboDistrito(ddlDepartamento1.value,this.value);">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodProvincia1" runat="server" value="" />
                    </td>
                    <td class="label">
                        Distrito
                    </td>
                    <td class="input">
                        <select id="ddlDistrito1" runat="server">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodDistrito1" runat="server" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Clasificación del Bien
                    </td>
                    <td class="input">
                        <input id="txtClasificacionBien0" type="text" class="css_input_inactivo" runat="server"
                            value="" style="width: 150px" />
                    </td>
                    <td class="label">
                        Tipo de Bien
                    </td>
                    <td class="input">
                        <select id="cmbTipoBien1" runat="server" name="cmbTipoBien1" onblur="fWidthCombo(0);"
                            style="width: 100px">
                            <option selected="selected">[-Seleccione-]</option>
                        </select>
                    </td>
                    <td class="label">
                        Estado del Bien
                    </td>
                    <td class="input">
                        <input id="txtEstadoBien1" type="text" runat="server" class="css_input_inactivo" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Moneda
                    </td>
                    <td class="input">
                        <select id="ddlMonedaBien1" name="D1" runat="server">
                            <option selected="selected">- Seleccione -</option>
                        </select>
                    </td>
                    <td class="label">
                        Valor del Bien
                    </td>
                    <td class="input">
                        <input id="txtValorBien1" type="text" runat="server" style="text-align: right;" value="" />
                    </td>
                    <td class="label">
                        Color
                    </td>
                    <td class="input">
                        <input id="txtColor1" type="text" runat="server" class="css_input" maxlength="20"
                            value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Placa Actual
                    </td>
                    <td class="input">
                        <input id="txtPlacaActual" type="text" runat="server" value="" />
                    </td>
                    <td class="label">
                        Placa Anterior
                    </td>
                    <td class="input">
                        <input id="txtPlacaAnterior" type="text" runat="server" value="" />
                    </td>
                    <td class="label">
                        Fecha de Transferencia
                    </td>
                    <td class="input">
                        <input id="txtFechaTransferencia1" runat="server" maxlength="10" type="text" class="css_input"
                            value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Año Fabricación
                    </td>
                    <td class="input">
                        <input id="txtAnio" type="text" runat="server" value="" />
                    </td>
                    <td class="label">
                        Nro. Serie
                    </td>
                    <td class="input">
                        <input id="txtNroSerie" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td class="label">
                        Nro. Motor
                    </td>
                    <td class="input">
                        <input id="txtNrMotor" type="text" runat="server" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Marca
                    </td>
                    <td class="input">
                        <input id="txtMarca" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td class="label">
                        Modelo
                    </td>
                    <td class="input">
                        <input id="txtModelo" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td class="input">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Tipo de Carrocería
                    </td>
                    <td class="input">
                        <input id="txtCarroceria" type="text" runat="server" value="" />
                    </td>
                    <td class="label">
                        Medidas
                    </td>
                    <td class="input">
                        <input id="txtMedidas" type="text" runat="server" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Observaciones
                    </td>
                    <td class="input" colspan="5">
                        <textarea id="txtObservaciones1" cols="20" runat="server" style="width: 99%; height: 43px;"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <!-- *********************************************************************
             Mantenimiento Otros
             *********************************************************************
        -->
        <div id="dv_datos_otros" class="dv_tabla_contenedora">
            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                <tr>
                    <td class="label">
                        Cantidad
                    </td>
                    <td class="input">
                        <input id="txtCantidad2" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td class="label">
                        Descripción
                    </td>
                    <td class="input" colspan="3">
                        <textarea id="txtDescripcionBien2"  runat="server" class="css_input_inactivo"
                            cols="50" rows="2" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Uso
                    </td>
                    <td class="input">
                        <input id="txtUso2" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td class="label">
                        Ubicación
                    </td>
                    <td class="input" colspan="3">
                        <input id="txtUbicacion2" type="text" runat="server" class="css_input_inactivo" value=""
                            style="width: 400px" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Departamento
                    </td>
                    <td class="input">
                        <select id="ddlDepartamento2" runat="server" onchange="javascript:fn_cargaComboProvinciaOtro(this.value);">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodDepartamento2" runat="server" value="" />
                    </td>
                    <td class="label">
                        Provincia
                    </td>
                    <td class="input">
                        <select id="ddlProvincia2" runat="server" onchange="javascript:fn_cargaComboDistritoOtro(ddlDepartamento2.value,this.value);">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodProvincia2" runat="server" value="" />
                    </td>
                    <td class="label">
                        Distrito
                    </td>
                    <td class="input">
                        <select id="ddlDistrito2" runat="server">
                            <option value="0">- Seleccionar -</option>
                        </select>
                        <input type="hidden" id="hidCodDistrito2" runat="server" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Clasificación del Bien
                    </td>
                    <td class="input">
                        <input id="txtClasificacionBien1" type="text" runat="server" class="css_input_inactivo"
                            value="" style="width: 150px" />
                    </td>
                    <td class="label">
                        Tipo de Bien
                    </td>
                    <td class="input">
                        <select id="cmbTipoBien2" runat="server" name="cmbTipoBien2" onblur="fWidthCombo(0);"
                            style="width: 100px">
                            <option selected="selected">[-Seleccione-]</option>
                        </select>
                    </td>
                    <td class="label">
                        Estado del Bien
                    </td>
                    <td class="input">
                        <input id="txtEstadoBien2" type="text" runat="server" class="css_input_inactivo"
                            value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Moneda
                    </td>
                    <td class="input">
                        <select id="ddlMonedaBien2" name="D1" runat="server">
                            <option selected="selected">- Seleccione -</option>
                        </select>
                    </td>
                    <td class="label">
                        Valor del Bien
                    </td>
                    <td class="input">
                        <input id="txtValorBien2" type="text" runat="server" style="text-align: right;" value="" />
                    </td>
                    <td class="label">
                        Color
                    </td>
                    <td class="input">
                        &nbsp;
                        <input id="txtColor2" type="text" runat="server" class="css_input" maxlength="20"
                            value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Partida Registral
                    </td>
                    <td class="input">
                        <input id="txtPartidaRegistral2" type="text" runat="server" value="" />
                    </td>
                    <td class="label">
                        Oficina Registral
                    </td>
                    <td class="input">
                        <input id="txtOficinaRegistral2" type="text" runat="server" value="" />
                    </td>
                    <td class="label">
                        Fecha de Transferencia
                    </td>
                    <td class="input">
                        <input id="txtFechaTransferencia2" maxlength="10" runat="server" type="text" class="css_input"
                            value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Marca
                    </td>
                    <td class="input">
                        <input id="txtMarca2" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                    <td class="label">
                        Modelo
                    </td>
                    <td class="input">
                        <input id="txtModelo2" type="text" runat="server" class="css_input_inactivo" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Observaciones
                    </td>
                    <td class="input" colspan="5">
                        <textarea id="txtObservaciones2" runat="server" cols="20" style="width: 99%; height: 43px;"></textarea>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div onmousedown="fWidthCombo(1)" style="text-align: left; overflow: hidden; position: absolute;
        top: 188px; left: 533px;">
        &nbsp;&nbsp;
    </div>
    <input id="hddCodclasibien" name="hddCodclasibien" type="hidden" runat="server" />
    <input id="hddCodContrato" name="hddCodContrato" type="hidden" runat="server" />
    <input id="hddSecFinanciamiento" name="hddSecFinanciamiento" type="hidden" runat="server" />
    <input id="hidCodTipoBien" name="hidCodTipoBien" type="hidden" runat="server" />
    </form>
</body>
</html>
