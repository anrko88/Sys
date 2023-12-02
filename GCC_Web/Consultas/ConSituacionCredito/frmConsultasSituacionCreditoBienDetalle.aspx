<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultasSituacionCreditoBienDetalle.aspx.vb"
    Inherits="Consultas_frmConsultasSituacionCreditoBienDetalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css" media="all" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_fuente.css" />
    
    <!-- JavaScript -->
    <script src="../../Util/js/jquery/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"></script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"></script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>
    <script type="text/javascript" src="../../Util/js/jquery/json2.js"></script>
    <script type="text/javascript" src="../../Util/js/js_global.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.modal.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.date.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script src="frmConsultasSituacionCreditoBienDetalle.aspx.js" type="text/javascript"></script>

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
    <input type="hidden" id="hidFlagOrigen" value="" runat="server" />
    <input type="hidden" id="HidCodEstado" value="" runat="server" />
    <input type="hidden" id="hidRowDocumento" value="" runat="server" />
    <input type="hidden" id="hidRowInafectacion" value="" runat="server" />
    <input type="hidden" id="hidRowInscripcionMunicipal" value="" runat="server" />
    <input type="hidden" id="hidCantidadRegistro" value="" runat="server" />
    <input type="hidden" id="hidMaxSecFinanciamiento" value="" runat="server" />
	<input type="hidden" id="hidPrecioVenta" value="" runat="server" />
    <input type="hidden" id="hidTotal" value="" runat="server" />
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="3">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_mantenimiento.jpg" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consultas</div>
                    <div class="css_lbl_titulo">
                        Detalle del Bien :: Detalle del Bien</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Volver();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <div id="Div1" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_botonNxt" class="dv_img_boton">
                        <a href="javascript:fn_Next();" id="hNext">
                            <img alt="" src="../../Util/images/ico_acc_retornar_back.gif" style="border: 0" /><br />
                            Avanzar </a>
                    </div>
                    <div id="dv_img_botonBck" class="dv_img_boton">
                        <a onclick="javascript:fn_Back();" id="hBack">
                            <img alt="" src="../../Util/images/ico_acc_retornar.gif" style="border: 0" /><br />
                            Retroceder </a>
                    </div>
                    
                    <%--JJM--%>
                    <div id="Div6" class="dv_img_boton" style="display: none">
                        <input id="btnBuscar" type="button" runat="server" onclick="javascript:fn_ListagrillaInafectacion();" />                        
                        <input id="cmdListarDocumentos" type="button" runat="server" />                        
                    </div>
                    <%--JJM--%>
                    
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
                        <!-- *********************************************************************
                              Datos del Contrato
                             *********************************************************************
                         -->
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contrato
                                </td>
                                <td class="botones">
                                    <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos_Cabecera" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nro. Contrato
                                    </td>
                                    <td class="input" runat="server" id="txtNumeroContrato">
                                        <%--<input id="txtNumeroContrato" name="txtNumeroContrato" runat="server" type="text"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />&nbsp;--%>
                                    </td>
                                    <td class="label" style="width: 150px">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input" id="txtclasificacion" runat="server">
                                       <%-- <input id="txtclasificacion" name="txtclasificacion" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" style="width: 150px" />--%>
                                    </td>
                                    <td class="label">
                                        Tipo del Bien
                                    </td>
                                    <td class="input" runat="server" id="txtTipoBien">
                                       <%-- <input id="txtTipoBien" name="txtTipoBien" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input" id="txtmoneda" runat="server">
                                        <%--<input id="txtmoneda" name="txtMoneda" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label">
                                        Estado del Contrato
                                    </td>
                                    <td class="input" runat="server" id="txtEstado">
                                        <%--<input id="txtEstado" name="txtEstado" type="text" runat="server" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label">
                                        Banca
                                    </td>
                                    <td class="input" id="txtBanca" runat="server">
                                        <%--<input id="txtBanca" name="txtBanca" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Ejecutivo
                                    </td>
                                    <td class="input" runat="server" id="txtEjecutivo" >
                                        <%--<input id="txtEjecutivo" name="txtEjecutivo" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label">
                                        Nro. Kardex
                                    </td>
                                    <td class="input" id="txtKardex" runat="server">
                                        <%--<input id="txtKardex" name="txtKardex" type="text" runat="server" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Observación del Contrato
                                    </td>
                                    <td class="input" colspan="5">
                                        <textarea id="txtObservacionContrato" name="txtObservacionContrato" runat="server"
                                             style="width: 585px" cols="50" type="text" rows="2"
                                            readonly="readonly" disabled="disabled" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!-- *********************************************************************
                              Datos del Cliente
                             *********************************************************************
                         -->
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Cliente
                                </td>
                                <td class="botones">
                                    <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos_Cabecera" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input" runat="server" id="txtcu">
                                        <%--<input id="txtcu" name="txtcu" type="text" runat="server" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label" style="width: 150px">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input" colspan="5" id="txtRazonSocial" runat="server">
                                        <%--<input id="txtRazonSocial" name="txtRazonSocial" runat="server" type="text" style="width: 150px"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label">
                                        Tipo Documento
                                    </td>
                                    <td class="input" runat="server" id="txtTipoDocumento">
                                        <%--<input id="txtTipoDocumento" name="txtTipoDocumento" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Nro. Documento
                                    </td>
                                    <td class="input" id="txtNumDocumento" runat="server">
                                        <%--<input id="txtNumDocumento" name="txtNumDocumento" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--
        			          DATOS DEL BIEN
        		        !-->
                        <!-- *********************************************************************
                         Mantenimiento de Bienes e Inmuebles
                         *********************************************************************
                    -->
                        <div id="dv_datos_inmueble" runat="server">
                            <div id="divTabs" style="border: 0; background: none;" class="dv_tabla_contenedora">
                                <ul>
                                    <li><a href="#tab-0">DATOS DEL BIEN</a></li>
                                    <li><a href="#tab-1">REGISTROS PUBLICOS Y MUNICIPAL</a></li>
                                    <li><a href="#tab-2">COMENTARIOS Y ADJUNTOS</a></li>
                                </ul>
                                <div id="tab-0">
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                        <colgroup>
                                            <col style="width: 10%;" />
                                            <col style="width: 22.33%;" />
                                            <col style="width: 13%;" />
                                            <col style="width: 23.33%;" />
                                            <col style="width: 29%;" />
                                            <col style="width: 21.33%;" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Departamento
                                            </td>
                                            <td class="input" runat="server" id="ddlDepartamentoInmueble">
                                                <%--<select id="ddlDepartamentoInmueble" runat="server" onchange="javascript:fn_cargaComboProvinciaInmueble(this.value);">
                                                    <%--<option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidCodDepartamentoInmueble" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Provincia
                                            </td>
                                            <td class="input" id="ddlProvinciaInmueble" runat="server" >
                                                <%--<select id="ddlProvinciaInmueble" runat="server" onchange="javascript:fn_cargaComboDistritoInmueble(ddlDepartamentoInmueble.value,this.value);">
                                                    <%--<option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidCodProvinciaInmueble" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Distrito
                                            </td>
                                            <td class="input" runat="server" id="ddlDistritoInmueble" >
                                                <%--<select id="ddlDistritoInmueble" runat="server">
                                                    <%--<option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidCodDistritoInmueble" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Ubicación
                                            </td>
                                            <td class="input" id="txtUbicacionInmueble" runat="server">
                                                <%--<input id="txtUbicacionInmueble" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Uso
                                            </td>
                                            <td class="input" runat="server" id="txtUsoInmueble">
                                               <%-- <input id="txtUsoInmueble" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Estado del Registro del Bien
                                            </td>
                                            <td class="input" id="ddlEstaRegistroBienInmueble" runat="server">
                                                <%--<select id="ddlEstaRegistroBienInmueble" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" >
                                                Tipo del Bien
                                            </td>
                                            <td class="input" runat="server" id="ddlTipoBien">
                                                <%--<select id="ddlTipoBien" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidTipoBien" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Moneda
                                            </td>
                                            <td class="input" id="ddlMonedaBien" runat="server">
                                                <%--<select id="ddlMonedaBien" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidMonedaBien" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Valor del Bien
                                            </td>
                                            <td class="input" runat="server" id="txtValorInmueble" >
                                              <%--  <input id="txtValorInmueble" name="txtValorInmueble" runat="server" type="text" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Estado del Bien
                                            </td>
                                            <td class="input" id="ddlEstadoBien" runat="server">
                                                <%--<select id="ddlEstadoBien" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidEstadoBien" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Descripción
                                            </td>
                                            <td class="input">
                                                <textarea id="txtDescripcionInmueble" name="txtDescripcionComun" runat="server" cols="30"
                                                    type="text" rows="2" disabled="disabled"  readonly="readonly" />
                                            </td>
                                            <td class="label">
                                                Cantidad
                                            </td>
                                            <td class="input" runat="server" id="txtCantidadInmueble">
                                               <%-- <input id="txtCantidadInmueble" name="txtcu" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Fecha de Transferencia
                                            </td>
                                            <td class="input" id="txtFechaTransferenciaInmueble" runat="server">
                                               <%-- <input id="txtFechaTransferenciaInmueble" name="txtFechaTransferenciaComun" type="text"
                                                    runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha de Adquisición
                                            </td>
                                            <td class="input" runat="server" id="txtFechaAdquisicionInmueble">
                                                <%--<input id="txtFechaAdquisicionInmueble" name="txtFechaAdquisicion" type="text" runat="server"
                                                    value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha de Baja
                                            </td>
                                            <td class="input" id="txtFechaBajaInmueble" runat="server">
                                                <%--<input id="txtFechaBajaInmueble" name="txtFechaBajaInmueble" type="text" runat="server"
                                                    value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" >
                                                Código del Predio
                                            </td>
                                            <td class="input" runat="server" id="txtCodigoPredio">
                                               <%-- <input id="txtCodigoPredio" name="txtCodigoPredio" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Observaciones
                                            </td>
                                            <td class="input" colspan="6">
                                                <textarea id="txtObservacionesInmueble" name="txtObservacionesComun" runat="server"
                                                    cols="108" type="text" rows="2" disabled="disabled"  readonly="readonly"  />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="tab-1">
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <colgroup>
                                            <col style="width: 10%;" />
                                            <col style="width: 45%;" />
                                            <col style="width: 30%;" />
                                            <col style="width: 15%;" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Fecha Probable de Fin de Obra
                                            </td>
                                            <td class="input" id="txtFechaProbableObra" runat="server">
                                                <%--<input id="txtFechaProbableObra" type="text" maxlength="10" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha Real de Fin de Obra
                                            </td>
                                            <td class="input" runat="server" id="txtFechaRealObra">
                                                <%--<input id="txtFechaRealObra" type="text" maxlength="10" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Fecha Inscripción Municipal
                                            </td>
                                            <td class="input" id="txtFechaInscripcionMunicipalInmueble" runat="server">
                                               <%-- <input id="txtFechaInscripcionMunicipalInmueble" type="text" maxlength="10" runat="server"
                                                    value="" />--%>
                                            </td>
                                            <td class="label">
                                                Estado Inscripción Municipal
                                            </td>
                                            <td class="input" runat="server" id="ddlEstadoMunicipalInmueble">
                                               <%-- <select id="ddlEstadoMunicipalInmueble" runat="server" name="ddlEstadoMunicipal">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input type="hidden" id="hidEstadoMunicipalInmueble" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Notaría
                                            </td>
                                            <td class="input" id="ddlNotariaInmueble" runat="server">
                                                <%--<select id="ddlNotariaInmueble" runat="server" name="ddlNotaria">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input type="hidden" id="hidCodNotaria" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha de Envío a Notaría
                                            </td>
                                            <td class="input" runat="server" id="txtFechaEnvioNotaria">
                                               <%-- <input id="txtFechaEnvioNotaria" type="text" maxlength="10" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Fecha Inscripción Registral
                                            </td>
                                            <td class="input" id="txtFechaInscripcionRegistralInmueble" runat="server" >
                                               <%-- <input id="txtFechaInscripcionRegistralInmueble" type="text" maxlength="10" runat="server"
                                                    value="" />--%>
                                            </td>
                                            <td class="label">
                                                Estado Inscripción Registral
                                            </td>
                                            <td class="input" runat="server"  id="ddlEstadoInscripcionRRPPInmueble">
                                                <%--<select id="ddlEstadoInscripcionRRPPInmueble" runat="server" name="ddlEstadoInscripcionRRPP">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input type="hidden" id="hidEstadoInscripcionRRPPInmueble" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Oficina Registral
                                            </td>
                                            <td class="input" id="txtOficinaRegistralInmueble" runat="server">
                                               <%-- <input id="txtOficinaRegistralInmueble" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha de Propiedad
                                            </td>
                                            <td class="input" runat="server" id="txtFechaPropiedad" >
                                                <%--<input id="txtFechaPropiedad" type="text" maxlength="10" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <%--                                        <tr>
                                            <td class="label">
                                                Propiedad
                                            </td>
                                            <td class="input">
                                            <select id="ddlPropiedadInmueble" runat="server" name="ddlPropiedadInmueble">
                                                        <option value='0'>[-Seleccione-]</option>
                                            </select>
                                            </td>
                                        </tr>
--%>
                                    </table>
                                    <input type="button" name="btnCargarInscripcion" id="btnCargarInscripcion" runat="server"
                                        style="display: none" onclick="javascript:fn_ListagrillaInscripcion();" />
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td colspan="6" align="left">
                                                <br />
                                              
                                                <div class="dv_img_boton_mini" id="divEditar" style="border: 0">
                                                    <a href="javascript:fn_EditarInscripcionMunicipal();">
                                                        <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                            border="0" />Ver Detalle </a>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" colspan="6">
                                                <table id="jqGrid_lista_A">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_A">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="tab-2">
                                    <input type="button" name="btnCargarAdjuntos" id="btnCargarAdjuntos" onclick="javascript:fn_ListagrillaDocumentos();"
                                        style="display: none" />
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <tr>
                                            <td colspan="6" align="left">
                                               
                                                <div class="dv_img_boton_mini" id="div7" style="border: 0">
                                                    <a href="javascript:fn_abreEditarDocumentoComentario();">
                                                        <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                            border="0" />Ver Detalle </a>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <table id="jqGrid_lista_C">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_C">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- *********************************************************************
                            VEHICULAR
                                 *********************************************************************
                            -->
                        <div id="dv_datos_vehiculo">
                            <div id="divTabsV" style="border: 0; background: none;" class="dv_tabla_contenedora">
                                <ul>
                                    <li><a href="#tab-3">DATOS DEL BIEN</a></li>
                                    <%--<li><a href="#tab-4">INMATRICULACIÓN</a></li>--%>
                                    <li><a href="#tab-5">REGISTROS PUBLICOS Y MUNICIPAL</a></li>
                                    <li><a href="#tab-6">INAFECTACIÓN</a></li>
                                </ul>
                                <div id="tab-3">
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                        <colgroup>
                                            <col style="width: 10%;" />
                                            <col style="width: 22.33%;" />
                                            <col style="width: 13%;" />
                                            <col style="width: 23.33%;" />
                                            <col style="width: 29%;" />
                                            <col style="width: 21.33%;" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Departamento
                                            </td>
                                            <td class="input" runat="server" id="ddlDepartamentoVehiculo">
                                                <%--<select id="ddlDepartamentoVehiculo" runat="server" onchange="javascript:fn_cargaComboProvinciaVehiculo(this.value);">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidDepartamentoVehiculo" runat="server" value="" />--%>
                                            </td>
                                            <td class="label" >
                                                Provincia
                                            </td>
                                            <td class="input" runat="server" id="ddlProvinciaVehiculo">
                                                <%--<select id="ddlProvinciaVehiculo" runat="server" onchange="javascript:fn_cargaComboDistritoVehiculo(ddlDepartamentoVehiculo.value,this.value);">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidProvinciaVehiculo" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Distrito
                                            </td>
                                            <td class="input" id="ddlDistritoVehiculo" runat="server">
                                                <%--<select id="ddlDistritoVehiculo" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidDistritoVehiculo" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Ubicación
                                            </td>
                                            <td class="input" runat="server" id="txtDireccionVehiculo">
                                              <%--  <input id="txtDireccionVehiculo" type="text" runat="server" />--%>
                                            </td>
                                            <td class="label">
                                                Uso
                                            </td>
                                            <td class="input" id="txtUsoVehiculo" runat="server">
                                                <%--<input id="txtUsoVehiculo" type="text" runat="server" />--%>
                                            </td>
                                            <td class="label">
                                                Estado del Registro del Bien
                                            </td>
                                            <td class="input" runat="server" id="ddlEstadoRegistroBienVehiculo">
                                                <%--<select id="ddlEstadoRegistroBienVehiculo" runat="server" name="ddlEstadoRegistroBienVehiculo">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Placa Actual
                                            </td>
                                            <td class="input" id="txtPlacaActualVehivulo" runat="server">
                                                <%--<input id="txtPlacaActualVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Placa Anterior
                                            </td>
                                            <td class="input" runat="server" id="txtPlacaAnteriorVehivulo">
                                               <%-- <input id="txtPlacaAnteriorVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Nro. Motor
                                            </td>
                                            <td class="input" id="txtNroMotorVehivulo" runat="server">
                                                <%--<input id="txtNroMotorVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Nro. Serie
                                            </td>
                                            <td class="input" runat="server" id="txtNroSerieVehivulo">
                                               <%-- <input id="txtNroSerieVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Marca
                                            </td>
                                            <td class="input" id="txtMarcaVehivulo" runat="server">
                                                <%--<input id="txtMarcaVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Año Fabricación
                                            </td>
                                            <td class="input" runat="server" id="txtAnioVehivulo">
                                                <%--<input id="txtAnioVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 150px">
                                                Tipo del Bien
                                            </td>
                                            <td class="input" id="ddlTipoBienVehiculo" runat="server">
                                               <%-- <select id="ddlTipoBienVehiculo" runat="server" name="ddlTipoMotor">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input id="hidTipoBienVehiculo" type="hidden" runat="server" />--%>
                                            </td>
                                            <td class="label">
                                                Modelo
                                            </td>
                                            <td class="input" runat="server" id="txtModeloVehivulo">
                                                <%--<input id="txtModeloVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Clase
                                            </td>
                                            <td class="input" id="ddlClaseVehivulo" runat="server">
                                                <%--<select id="ddlClaseVehivulo" runat="server" name="ddlClase">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input id="txtClaseVehiculo" type="text" runat="server" value="" />
                                                <input id="hidCodClaseVehivulo" type="hidden" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 150px">
                                                Moneda
                                            </td>
                                            <td class="input" runat="server" id="ddlMonedaVehiculo">
                                                <%--<select id="ddlMonedaVehiculo" runat="server" name="ddlTipoMotor">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input id="hidMonedaVehiculo" type="hidden" runat="server" />--%>
                                            </td>
                                            <td class="label" style="width: 150px">
                                                Valor del Bien
                                            </td>
                                            <td class="input" id="txtValorVehivulo" runat="server">
                                                <%--input id="txtValorVehivulo" name="txtValorComun" runat="server" type="text" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Estado del Bien
                                            </td>
                                            <td class="input" runat="server" id="ddlEstadoBienVehiculo">
                                                <%--<select id="ddlEstadoBienVehiculo" runat="server" name="ddlTipoMotor">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input id="hidEstadoBienVehiculo" type="hidden" runat="server" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Descripción
                                            </td>
                                            <td class="input">
                                                <textarea id="txtDescripcionVehivulo" name="txtDescripcionComun" runat="server" style="width: 200px"
                                                    cols="50" type="text" rows="2" readonly="readonly" disabled="disabled" />
                                            </td>
                                            <td class="label">
                                                Cantidad
                                            </td>
                                            <td class="input" runat="server" id="txtCantidadVehivulo">
                                                <%--<input id="txtCantidadVehivulo" name="txtcu" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Color
                                            </td>
                                            <td class="input" id="txtColorVehivulo" runat="server">
                                                <%--input id="txtColorVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Medidas
                                            </td>
                                            <td class="input" runat="server" id="txtMedidasVehivulo">
                                                <%--<input id="txtMedidasVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Tipo Carrocería
                                            </td>
                                            <td class="input" id="txtCarroceriaVehiculo" runat="server">
                                                <%--<select id="ddlTipoCarroceriaVehiculo" runat="server" name="ddlTipoMotor">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>--%>
                                                <%--<input id="txtCarroceriaVehiculo" type="text" runat="server" />--%>
                                            </td>
                                        </tr>
                                        <tr>                                          
                                            <td class="label">
                                                Transmisión
                                            </td>
                                            <td class="input" id="ddlTransmisionVehivulo" runat="server">
                                                <%--<select id="ddlTransmisionVehivulo" runat="server" name="ddlTransmision">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input id="hidTransmisionVehiculo" type="hidden" runat="server" />--%>
                                            </td>
                                            <td class="label">
                                                Tracción
                                            </td>
                                            <td class="input" id="ddlTraccionVehivulo" runat="server">
                                                <%--<select id="ddlTraccionVehivulo" runat="server" name="ddlTraccion">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input id="hidTraccionVehiculo" type="hidden" runat="server" />--%>
                                            </td>
                                            <td class="label">
                                                Tipo de Motor
                                            </td>
                                            <td class="input" id="ddlTipoMotorVehivulo" runat="server">
                                                <%--<select id="ddlTipoMotorVehivulo" runat="server" name="ddlTipoMotor">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input id="hidTipoMotorVehiculo" type="hidden" runat="server" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Potencia de Motor
                                            </td>
                                            <td class="input" id="txtPotenciaMotorVehivulo" runat="server">
                                                <%--<input id="txtPotenciaMotorVehivulo" type="text" runat="server" class="css_input"
                                                    value="" />--%>
                                            </td>
                                            <td class="label">
                                                Combustible
                                            </td>
                                            <td class="input" id="txtCombustibleVehiculo" runat="server">
                                                <%-- <select id="ddlCombustibleVehivulo" runat="server" name="ddlTipoMotor">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>--%>
                                                <%--<input id="txtCombustibleVehiculo" type="text" runat="server" class="css_input" value="" />
                                                <input id="hidCombustibleVehiculo" type="hidden" runat="server" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Longitud
                                            </td>
                                            <td class="input" id="txtLongitudVehivulo" runat="server">
                                                <%--<input id="txtLongitudVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Ancho
                                            </td>
                                            <td class="input" id="txtAnchoVehivulo" runat="server">
                                               <%-- <input id="txtAnchoVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Alto
                                            </td>
                                            <td class="input" id="txtAltoVehivulo" runat="server">
                                               <%-- <input id="txtAltoVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Puertas
                                            </td>
                                            <td class="input" id="txtPuertasVehivulo" runat="server">
                                                <%--<input id="txtPuertasVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Asientos
                                            </td>
                                            <td class="input" id="txtAsientosVehivulo" runat="server">
                                                <%--<input id="txtAsientosVehivulo" type="text" runat="server" />--%>
                                            </td>
                                            <td class="label">
                                                Pasajeros
                                            </td>
                                            <td class="input" id="txtPasajerosVehivulo" runat="server">
                                                <%--<input id="txtPasajerosVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Ruedas
                                            </td>
                                            <td class="input" id="txtRuedasVehivulo" runat="server">
                                                <%--<input id="txtRuedasVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Ejes
                                            </td>
                                            <td class="input" id="txtEjesVehivulo" runat="server">
                                                <%--<input id="txtEjesVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fórmula Rodante
                                            </td>
                                            <td class="input" id="txtFormulaRodanteVehivulo" runat="server">
                                               <%-- <input id="txtFormulaRodanteVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Peso Bruto
                                            </td>
                                            <td class="input" id="txtPesoBrutoVehivulo" runat="server">
                                                <%--<input id="txtPesoBrutoVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Peso Neto
                                            </td>
                                            <td class="input" id="txtPesoNetoVehivulo" runat="server">
                                               <%-- <input id="txtPesoNetoVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Carga Útil
                                            </td>
                                            <td class="input" id="txtCargaUtilVehivulo" runat="server">
                                                <%--<input id="txtCargaUtilVehivulo" type="text" runat="server" class="css_input" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Cilindros
                                            </td>
                                            <td class="input" id="txtCilindrosVehivulo" runat="server">
                                               <%-- <input id="txtCilindrosVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Cilindrada
                                            </td>
                                            <td class="input" id="txtCilindrajeVehivulo" runat="server">
                                               <%-- <input id="txtCilindrajeVehivulo" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Observaciones
                                            </td>
                                            <td class="input" colspan="5">
                                                <textarea id="txtObservacionesVehivulo" name="txtObservacionesComun" runat="server"
                                                    style="width: 680px" cols="50" type="text" rows="2" readonly="readonly" disabled="disabled" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <!-- *********************************************************************
                                     REGISTROS PUBLICOS
                                 *********************************************************************
                             -->
                                <div id="tab-5">
								 <!-- IBK - RPH -->
                                   <fieldset id="fsRegistral">
                                    <legend>Registral</legend>
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                         <colgroup>

                                        <col style="width: 14%;" />
                                        <col style="width: 18.33%;" />

                                        <col style="width: 14%;" />
                                        <col style="width: 20.33%;" />

                                        <col style="width: 20%;" />
                                        <col style="width: 21.33%;" />
                                        </colgroup>
                                        
                                        <tr>
                                            <td class="label">

                                                Fecha de Adquisición
                                            </td>
                                            <td class="input" id="txtFechaAdquisionVehiculo" runat="server" >
<%--
                                                <input id="txtFechaAdquisionVehiculo" name="txtFechaAdquisicion" type="text" runat="server"
                                                    value="" />--%>
                                            </td>
                                            <td class="label">

                                                Estado Inscripción Registral
                                            </td>
                                            <td class="input" id="ddlEstadoInscripcionRRPPVehiculo" runat="server">


                                                <%--<select id="ddlEstadoInscripcionRRPPVehiculo" runat="server" name="ddlEstadoInscripcionRRPP">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input type="hidden" id="hidEstadoInscripcionRRPP" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">

                                                Fecha Inscripción Registral
                                            </td>
                                            <td class="input" id="txtFechaInscripcionRegistralVehivulo" runat="server">

                                                <%--
                                                <input id="txtFechaInscripcionRegistralVehivulo" name="txtFechaInscripcionRegistral"
                                                    runat="server" type="text" />--%>
                                            </td>
                                            
                                        </tr>
                                        <tr> 
                                            <td class="label">
                                                Fecha de Propiedad
                                            </td>
                                            <td class="input" id="txtFechaPropiedadVehivulo" runat="server">
                                               <%-- <input id="txtFechaPropiedadVehivulo" name="txtFechaPropiedad" runat="server" type="text" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha Emisión Tarjeta de Propiedad
                                            </td>
                                            <td class="input" id="txtFechaEmisionTarjetaVehiculo" runat="server">


                                                <%--<input id="txtFechaEmisionTarjetaVehiculo" name="txtFechaEmisionTarjetaVehiculo" runat="server" type="text" />--%>
                                            </td>
                                            <td class="label">

                                                Fecha de Transferencia
                                            </td>
                                            <td class="input" id="txtFechaTransferenciaVehivulo" runat="server">

                                                <%--<input id="txtFechaTransferenciaVehivulo" name="txtFechaTransferenciaComun" type="text"
                                                    runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td class="label" >
                                                Fecha de Envío a Notaría
                                            </td>
                                            <td class="input" id="txtFechaEnvioNotariaVehivulo" runat="server">
                                                <%--<input id="txtFechaEnvioNotariaVehivulo" type="text" runat="server" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                    </fieldset>
                                    <fieldset id="fsMunicipal">
                                    <legend>Municipal</legend>
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                         <colgroup>
                                        <col style="width: 14%;" />
                                        <col style="width: 18.33%;" />
                                        <col style="width: 14%;" />
                                        <col style="width: 20.33%;" />
                                        <col style="width: 20%;" />
                                        <col style="width: 21.33%;" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">

                                                Estado Inscripción Municipal
                                            </td>
                                            <td class="input" id="ddlEstadoMunicipalVehiculo" runat="server">


                                                <%--<select id="ddlEstadoMunicipalVehiculo" runat="server" name="ddlEstadoMunicipalVehiculo">
                                                    <option value='0'>[-Seleccione-]</option>
                                                </select>
                                                <input type="hidden" id="hidEstadoMunicipalVehiculo" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">

                                                Fecha Inscripción Municipal
                                            </td>
                                            <td class="input" id="txtFechaInscripcionMunicipalVehivulo" runat="server">




                                                <%--<input id="txtFechaInscripcionMunicipalVehivulo" name="txtFechaInscripcionMunicipal"
                                                    type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha de Envío a SAT
                                            </td>
                                            <td class="input" id="txtFechaEnvioSATVehivulo" runat="server">
                                                <%--<input id="txtFechaEnvioSATVehivulo" type="text" runat="server" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Fecha de Baja
                                            </td>
                                            <td class="input" id="txtFechaBajaVehiculo" runat="server">
                                                <%--<input id="txtFechaBajaVehiculo" name="txtFechaBajaVehiculo" type="text" runat="server"
                                                    value="" />--%>
                                            </td>
                                        </tr>
                                        </table>
                                    </fieldset> 
                                </div>
                                <!-- *********************************************************************
                                     INAFECTACIÓN
                                 *********************************************************************
                            -->
                                <div id="tab-6">
                                    <input id="btnCargarInafectacion" type="button" name="btnCargarInafectacion" style="display: none"
                                        onclick="javascript:fn_ListagrillaInafectacion();" />
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <tr>
                                            <td class="label">
                                                Inafectación
                                            </td>
                                            <td class="input" colspan="3">
                                                <input id="cbInafectacion" type="checkbox" runat="server" readonly="readonly" disabled="disabled" />
                                            </td>
                                            <td class="label">
                                                Pago de Impuestos
                                            </td>
                                            <td class="input" colspan="3">
                                                <input id="cbPagoImpuestos" type="checkbox" runat="server" disabled="disabled" readonly="readonly" />
                                            </td>
                                            <td>
                                            </td>
                                            <td colspan="6" align="left">
                                                <br />
                                              
                                                <div class="dv_img_boton_mini" id="div3" style="border: 0">
                                                    <a href="javascript:fn_EditarInafectacion();">
                                                        <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                            border="0" />Ver Detalle </a>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <%--<tr id="btn_botones_inafectacion" runat="server">
                                            
                                        </tr>--%>
                                        <tr>
                                            <td valign="top" colspan="6">
                                                <div id="dv_grilla_inafectacion">
                                                    <table id="jqGrid_lista_B">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="jqGrid_pager_B" align="center">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- *********************************************************************
                            SISTEMA DE PROCESAMIENTO ELECTRONICO DE DATOS - OTROS
                             *********************************************************************
                        -->
                        <div id="dv_datos_otros">
                            <div id="divTabsS" style="border: 0; background: none;" class="dv_tabla_contenedora">
                                <ul>
                                    <li><a href="#tab-7">DATOS DEL BIEN</a></li>
                                </ul>
                                <div id="tab-7">
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                        <colgroup>
                                            <col style="width: 10%;" />
                                            <col style="width: 22.33%;" />
                                            <col style="width: 13%;" />
                                            <col style="width: 23.33%;" />
                                            <col style="width: 29%;" />
                                            <col style="width: 21.33%;" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Departamento
                                            </td>
                                            <td class="input" id="ddlDepartamentoOtros" runat="server"></td>
                                            <td class="label">
                                                Provincia
                                            </td>
                                            <td class="input" id="ddlProvinciaOtros" runat="server">
                                                
                                            </td>
                                            <td class="label">
                                                Distrito
                                            </td>
                                            <td class="input" id="ddlDistritoOtros" runat="server">                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Ubicación
                                            </td>
                                            <td class="input" id="txtUbicacionOtros" runat="server">                                                
                                            </td>
                                            <td class="label">
                                                Uso
                                            </td>
                                            <td class="input" id="txtUsoOtros" runat="server">
                                               
                                            </td>
                                            <td class="label">
                                                Estado del Registro del Bien
                                            </td>
                                            <td class="input" id="ddlEstadoRegistroBienOtros" runat="server">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Nro. Serie
                                            </td>
                                            <td class="input" id="txtSerieOtros" runat="server">
                                               
                                            </td>
                                            <td class="label">
                                                Marca
                                            </td>
                                            <td class="input" id="txtMarcaOtros" runat="server">                                               
                                            </td>
                                            <td class="label">
                                                Nro. Motor
                                            </td>
                                            <td class="input" id="txtMotorOtros" runat="server">
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 150px">
                                                Tipo del Bien
                                            </td>
                                            <td class="input" id="ddlTipoBienOtros" runat="server">                                                
                                            </td>
                                            <td class="label">
                                                Modelo
                                            </td>
                                            <td class="input" id="txtModeloOtros" runat="server">                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 150px">
                                                Moneda
                                            </td>
                                            <td class="input" id="ddlMonedaOtros" runat="server">                                                
                                            </td>
                                            <td class="label" style="width: 150px">
                                                Valor del Bien
                                            </td>
                                            <td class="input" id="txtValorBienOtros" runat="server">                                                
                                            </td>
                                            <td class="label">
                                                Estado del Bien
                                            </td>
                                            <td class="input" id="ddlBienOtros" runat="server">                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Descripción
                                            </td>
                                            <td class="input">
                                                <textarea id="txtDescripcionOtros" name="txtDescripcionComun" runat="server" style="width: 200px"
                                                    cols="50" type="text" rows="2" readonly="readonly" disabled="disabled" />
                                            </td>
                                            <td class="label">
                                                Cantidad
                                            </td>
                                            <td class="input" id="txtCantidadOtros" runat="server">
                                               
                                            </td>
                                            <td class="label">
                                                Color
                                            </td>
                                            <td class="input" id="txtColorOtros" runat="server">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Fecha de Transferencia
                                            </td>
                                            <td class="input" id="txtFechaTransferenciaOtros" runat="server">
                                               
                                            </td>
                                            <td class="label">
                                                Fecha de Adquisición
                                            </td>
                                            <td class="input" id="txtFechaAdquisicionOtros" runat="server">                                                
                                            </td>
                                            <td class="label">
                                                Fecha de Baja
                                            </td>
                                            <td class="input" id="txtFechaBajaOtros" runat="server">                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Partida Registral
                                            </td>
                                            <td class="input" id="txtPartidaRegistralOtros" runat="server">                                               
                                            </td>
                                            <td class="label">
                                                Oficina Registral
                                            </td>
                                            <td class="input" id="txtOficinaRegistralOtros" runat="server">                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Observaciones
                                            </td>
                                            <td class="input" colspan="5">
                                                <textarea id="txtObservacionesOtros" name="txtObservacionesComun" runat="server"
                                                    style="width: 680px" cols="50" type="text" rows="2" readonly="readonly" disabled="disabled" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- *********************************************************************
                            MAQUINARIA
                             *********************************************************************
                        -->
                        <div id="dv_datos_maquinaria">
                            <div id="divTabsM" style="border: 0; background: none;" class="dv_tabla_contenedora">
                                <ul>
                                    <li><a href="#tab-8">DATOS DEL BIEN</a></li>
                                </ul>
                                <div id="tab-8">
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                        <colgroup>
                                            <col style="width: 10%;" />
                                            <col style="width: 22.33%;" />
                                            <col style="width: 13%;" />
                                            <col style="width: 23.33%;" />
                                            <col style="width: 29%;" />
                                            <col style="width: 21.33%;" />
                                        </colgroup>
                                        <tr>
                                            <td class="label">
                                                Departamento
                                            </td>
                                            <td class="input" runat="server" id="ddlDepartamentoMaquinaria">
                                                <%--<select id="ddlDepartamentoMaquinaria" runat="server" onchange="javascript:fn_cargaComboProvinciaMaquinaria(this.value);"
                                                    style="width: 125px">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidDepartamentoMaquinaria" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Provincia
                                            </td>
                                            <td class="input" id="ddlProvinciaMaquinaria" runat="server">
                                                <%--<select id="ddlProvinciaMaquinaria" runat="server" onchange="javascript:fn_cargaComboDistritoMaquinaria(ddlDepartamentoMaquinaria.value,this.value);">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidProvinciaMaquinaria" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Distrito
                                            </td>
                                            <td class="input" runat="server" id="ddlDistritoMaquinaria">
                                                <%--<select id="ddlDistritoMaquinaria" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidDistritoMaquinaria" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Ubicación
                                            </td>
                                            <td class="input" id="txtDireccionMaquinaria" runat="server">
                                               <%-- <input id="txtDireccionMaquinaria" type="text" runat="server" />--%>
                                            </td>
                                            <td class="label">
                                                Uso
                                            </td>
                                            <td class="input" runat="server" id="txtUsoMaquinaria" >
                                               <%-- <input id="txtUsoMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Estado del Registro del Bien
                                            </td>
                                            <td class="input" id="ddlEstadoRegistroBienMaquinaria" runat="server" >
                                                <%--<select id="ddlEstadoRegistroBienMaquinaria" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Placa Actual
                                            </td>
                                            <td class="input" runat="server" id="txtPlacaActualMaquinaria">
                                               <%-- <input id="txtPlacaActualMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Placa Anterior
                                            </td>
                                            <td class="input" id="txtPlacaAnteriorMaquinaria" runat="server">
                                               <%-- <input id="txtPlacaAnteriorMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Nro. Motor
                                            </td>
                                            <td class="input" id="txtMotorMaquinaria" runat="server">
                                                <%--<input id="txtMotorMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Nro. Serie
                                            </td>
                                            <td class="input" id="txtSerieMaquinaria" runat="server">
                                                <%--<input id="txtSerieMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Marca
                                            </td>
                                            <td class="input" id="txtMarcaMaquinaria" runat="server">
                                                <%--<input id="txtMarcaMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Año de Fabricación
                                            </td>
                                            <td class="input" id="txtAnioTransferenciaMaquinaria" runat="server">
                                                <%--<input id="txtAnioTransferenciaMaquinaria" type="text" runat="server" class="css_input"
                                                    value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Tipo del Bien
                                            </td>
                                            <td class="input" id="ddlTipoBienMaquinaria" runat="server">
                                                <%--<select id="ddlTipoBienMaquinaria" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidTipoBienMaquinaria" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Modelo
                                            </td>
                                            <td class="input" id="txtModeloMaquinaria" runat="server">
                                                <%--<input id="txtModeloMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Moneda
                                            </td>
                                            <td class="input" id="ddlMonedaMaquinaria" runat="server">
                                                <%--<select id="ddlMonedaMaquinaria" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidMonedaMaquinaria" runat="server" value="" />--%>
                                            </td>
                                            <td class="label" style="width: 150px">
                                                Valor del Bien
                                            </td>
                                            <td class="input" id="txtValorBienMaquinaria" runat="server">
                                                <%--<input id="txtValorBienMaquinaria" name="txtValorComun" runat="server" type="text"
                                                    value="" />--%>
                                            </td>
                                            <td class="label">
                                                Estado del Bien
                                            </td>
                                            <td class="input" id="ddlEstadoBienMaquinaria" runat="server">
                                                <%--<select id="ddlEstadoBienMaquinaria" runat="server">
                                                    <option value="0">- Seleccionar -</option>
                                                </select>
                                                <input type="hidden" id="hidEstadoBienMaquinaria" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Descripción
                                            </td>
                                            <td class="input">
                                                <textarea id="txtDescripcionMaquinaria" name="txtDescripcionComun" runat="server"
                                                    style="width: 200px" cols="50" type="text" rows="2" disabled="disabled" readonly="readonly" />
                                            </td>
                                            <td class="label">
                                                Cantidad
                                            </td>
                                            <td class="input" id="txtCantidadMaquinaria" runat="server">
                                                <%--<input id="txtCantidadMaquinaria" name="txtcu" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Color
                                            </td>
                                            <td class="input" id="txtColorMaquinaria" runat="server">
                                              <%--  <input id="txtColorMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Medidas
                                            </td>
                                            <td class="input" id="txtMedidasMaquinaria" runat="server">
                                                <%--<input id="txtMedidasMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Tipo Carroceria
                                            </td>
                                            <td class="input" id="txtCarroceriaMaquinaria" runat="server">
                                               <%-- <input id="txtCarroceriaMaquinaria" type="text" runat="server" value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Fecha de Transferencia
                                            </td>
                                            <td class="input" id="txtFechaTransferenciaMaquinaria" runat="server">
                                                <%--<input id="txtFechaTransferenciaMaquinaria" name="txtFechaTransferenciaComun" type="text"
                                                    runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha de Adquisición
                                            </td>
                                            <td class="input" id="txtFechaAdquisicionMaquinaria" runat="server">
                                                <%--<input id="txtFechaAdquisicionMaquinaria" name="txtFechaAdquisicion" type="text"
                                                    runat="server" value="" />--%>
                                            </td>
                                            <td class="label">
                                                Fecha de Baja
                                            </td>
                                            <td class="input" id="txtFechaBajaMaquinaria" runat="server">
                                                <%--<input id="txtFechaBajaMaquinaria" name="txtFechaBajaMaquinaria" type="text" runat="server"
                                                    value="" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Observaciones
                                            </td>
                                            <td class="input" colspan="5">
                                                <textarea id="txtObservacionesMaquinaria" name="txtObservacionesComun" runat="server"
                                                    style="width: 680px" cols="50" type="text" rows="2" disabled="disabled" readonly="readonly" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
