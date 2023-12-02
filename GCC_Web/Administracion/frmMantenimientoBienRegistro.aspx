<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoBienRegistro.aspx.vb"
    Inherits="Administracion_frmMantenimientoBienRegistro" %>

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

    <script src="frmMantenimientoBienRegistro.aspx.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmDatosBien" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <input type="hidden" id="hidNumeroContrato" value="" runat="server" />
    <input type="hidden" id="hidSecFinanciamiento" value="0" runat="server" />
    <input type="hidden" id="hidOp" value="0" runat="server" />
    <input type="hidden" id="hidCodClasificacion" value="" runat="server" />
    <input type="hidden" id="hidCodTipoBien" value="" runat="server" />
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="3">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Mantenimiento de Bien</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <div id="Div1" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:javascript:fn_grabar();" id="hGrabar">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="3">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contrato
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos_Cabecera" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNumeroContrato" name="txtNumeroContrato" runat="server" type="text"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />
                                    </td>
                                    <td class="label">
                                        Estado del Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtEstado" name="txtEstado" type="text" runat="server" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />
                                    </td>
                                    <td class="label">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtclasificacion" name="txtclasificacion" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtcu" name="txtcu" type="text" runat="server" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />
                                        
                                    </td>
                                    <td class="label" style="width: 150px">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" name="txtRazonSocial" runat="server" type="text" style="width: 250px"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />
                                    </td>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <input id="txtmoneda" name="txtMoneda" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="Table1" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Bien
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
                                        <input id="txtCantidad" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" style="width:120px" />
                                    </td>
                                    <td class="label">
                                        Descripción
                                    </td>
                                    <td class="input">
                                        <textarea id="txtDescripcionDemanda" runat="server" class="css_input_inactivo"
                                            cols="50" rows="2" />
                                    </td>
                                    <td class="label">
                                        Estado del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoBien" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" style="width:100px "/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Uso
                                    </td>
                                    <td class="input">
                                        <input id="txtUso" type="text" class="css_input_inactivo" runat="server" size="50"
                                            value="" style="width:120px " />
                                    </td>
                                    <td class="label">
                                        Ubicación
                                    </td>
                                    <td class="input">
                                        <input id="txtUbicacion" type="text" class="css_input_inactivo" runat="server" size="50"
                                            value="" />
                                    </td>
                                   <%-- <td class="label">
                                        Color
                                    </td>
                                    <td class="input">
                                        <input id="txtColor1" type="text" runat="server" class="css_input" size="50" value="" />
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td class="label" >
                                        Departamento
                                    </td>
                                    <td class="input">
                                        <select id="ddlDepartamento" runat="server" onchange="javascript:fn_cargaComboProvinciaBien(this.value);" style="width:125px ">
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
                                        Tipo de Bien
                                    </td>
                                    <td class="input" class="css_input">
                                        <select id="cmbTipoBien" name="D1" style="width:125px ">
                                            <option selected="selected">- Seleccione -</option>
                                        </select>
                                    </td>
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
                                        <input id="txtValorBien" type="text" style="text-align: right;" runat="server" size="50"
                                            value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Partida Registral
                                    </td>
                                    <td class="input">
                                        <input id="txtPartidaRegistral" type="text" maxlength="10" runat="server" size="50"
                                            value="" style="width:120px "/>
                                    </td>
                                    <td class="label">
                                        Oficina Registral
                                    </td>
                                    <td class="input">
                                        <input id="txtOficinaRegistral" type="text" runat="server" size="50" value="" />
                                    </td>
                                    <td class="label">
                                        Fecha de Transferencia
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaTransferencia" maxlength="10" type="text" class="css_input" runat="server"
                                            size="11" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Observaciones
                                    </td>
                                    <td class="input" colspan="5">
                                        <textarea id="txtObservaciones" cols="20" runat="server" runat="server" style="width: 100%;
                                            height: 43px;"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!-- *********************************************************************
                         Mantenimiento de Bienes e Inmuebles
                         *********************************************************************
                    -->
                        <div id="dv_datos_vehiculo" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Cantidad
                                    </td>
                                    <td class="input">
                                        <input id="txtCantidad1" type="text" runat="server" class="css_input_inactivo" size="50" style="width:120px "
                                            value="" tyle="width:120px " />
                                    </td>
                                    <td class="label">
                                        Descripción
                                    </td>
                                    <td class="input">
                                        <textarea id="txtDescripcionBien1" runat="server" class="css_input_inactivo"
                                            cols="50" rows="2" />
                                    </td>
                                    <td class="label">
                                        Estado del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoBien1" type="text" runat="server" class="css_input_inactivo"
                                            size="50" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Uso
                                    </td>
                                    <td class="input">
                                        <input id="txtUso1" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" style="width:120px "/>
                                    </td>
                                    <td class="label">
                                        Ubicación
                                    </td>
                                    <td class="input">
                                        <input id="txtUbicacion1" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" />
                                    </td>
                                     <td class="label">
                                        Color
                                    </td>
                                    <td class="input">
                                        <input id="txtColor" type="text" runat="server" class="css_input" size="50" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Departamento
                                    </td>
                                    <td class="input">
                                        <select id="ddlDepartamento1" runat="server" onchange="javascript:fn_cargaComboProvincia(this.value);" style="width:125px ">
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
                                    <td class="label" >
                                        Tipo de Bien
                                    </td>
                                    <td class="input" class="css_input">
                                        <select id="cmbTipoBien1" style="width:125px ">
                                            <option selected="selected">[-Seleccione-]</option>
                                        </select>
                                    </td>
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
                                        <input id="txtValorBien1" type="text" runat="server" style="text-align: right" size="50"
                                            value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Placa Actual
                                    </td>
                                    <td class="input">
                                        <input id="txtPlacaActual" type="text" runat="server" size="50" value="" style="width:120px " />
                                    </td>
                                    <td class="label">
                                        Placa Anterior
                                    </td>
                                    <td class="input">
                                        <input id="txtPlacaAnterior" type="text" runat="server" 
                                            size="50" value="" />
                                    </td>
                                    <td class="label">
                                        Fecha de Transferencia
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaTransferencia1" runat="server" maxlength="10" type="text" class="css_input"
                                            size="11" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Año Fabricación
                                    </td>
                                    <td class="input">
                                        <input id="txtAnio" type="text" runat="server" size="50" value="" style="width:120px " />
                                    </td>
                                    <td class="label">
                                        Nro. Serie
                                    </td>
                                    <td class="input">
                                        <input id="txtNroSerie" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" />
                                    </td>
                                    <td class="label">
                                        Nro. Motor
                                    </td>
                                    <td class="input">
                                        <input id="txtNrMotor" type="text" runat="server" size="50" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Marca
                                    </td>
                                    <td class="input">
                                        <input id="txtMarca" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" style="width:120px " />
                                    </td>
                                    <td class="label">
                                        Modelo
                                    </td>
                                    <td class="input">
                                        <input id="txtModelo" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" />
                                    </td>
                                   <td class="label">
                                        Tipo de Carrocería
                                    </td>
                                    <td class="input">
                                        <input id="txtCarroceria" type="text" runat="server" value="" style="width:120px " />
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td class="label">
                                        Medidas
                                    </td>
                                    <td class="input">
                                        <input id="txtMedidas" type="text" runat="server" size="50" value="" style="width:120px "/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Observaciones
                                    </td>
                                    <td class="input" colspan="5">
                                        <textarea id="txtObservaciones1" runat="server" style="width: 100%; height: 43px;"></textarea>
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
                                        <input id="txtCantidad2" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" style="width:120px "/>
                                    </td>
                                    <td class="label">
                                        Descripción
                                    </td>
                                    <td class="input">
                                        <textarea id="txtDescripcionBien2"  runat="server" class="css_input_inactivo"
                                            cols="50" rows="2" />
                                    </td>
                                    <td class="label" >
                                        Estado del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoBien2" type="text" runat="server" class="css_input_inactivo"
                                            size="50" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Uso
                                    </td>
                                    <td class="input">
                                        <input id="txtUso2" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" style="width:120px "/>
                                    </td>
                                    <td class="label">
                                        Ubicación
                                    </td>
                                    <td class="input">
                                        <input id="txtUbicacion2" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" />
                                    </td>
                                    <td class="label">
                                        Color
                                    </td>
                                    <td class="input">
                                        <input id="txtColor2" type="text" runat="server" class="css_input" size="50" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Departamento
                                    </td>
                                    <td class="input">
                                        <select id="ddlDepartamento2" runat="server" onchange="javascript:fn_cargaComboProvinciaOtro(this.value);" style="width:125px ">
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
                                        Tipo de Bien
                                    </td>
                                    <td class="input" class="css_input">
                                        <select id="cmbTipoBien2" style="width:125px ">
                                            <option selected="selected">[-Seleccione-]</option>
                                        </select>
                                    </td>
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
                                        <input id="txtValorBien2" type="text" runat="server" style="text-align: right;" size="50"
                                            value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Partida Registral
                                    </td>
                                    <td class="input">
                                        <input id="txtPartidaRegistral2" type="text" maxlength="10" runat="server" size="50"
                                            value="" style="width:120px "/>
                                    </td>
                                    <td class="label">
                                        Oficina Registral
                                    </td>
                                    <td class="input">
                                        <input id="txtOficinaRegistral2" type="text" runat="server" size="50" value="" />
                                    </td>
                                    <td class="label">
                                        Fecha de Transferencia
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaTransferencia2" maxlength="10" runat="server" type="text" class="css_input"
                                            size="11" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Marca
                                    </td>
                                    <td class="input">
                                        <input id="txtMarca2" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" style="width:120px "/>
                                    </td>
                                    <td class="label">
                                        Modelo
                                    </td>
                                    <td class="input">
                                        <input id="txtModelo2" type="text" runat="server" class="css_input_inactivo" size="50"
                                            value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Observaciones
                                    </td>
                                    <td class="input" colspan="5">
                                        <textarea id="txtObservaciones2" runat="server" style="width: 100%; height: 43px;"></textarea>
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Proveedores
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora">
                            <table id="jqGrid_lista_F">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                        </div>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
