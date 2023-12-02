<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultasBienContrato.aspx.vb"
    Inherits="Consultas_frmConsultasBienContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <script src="frmConsultasBienContrato.aspx.js" type="text/javascript"></script>

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
    <input type="hidden" id="hddRowId" value="" runat="server" />
    <input type="hidden" id="hidFlagOrigen" value="" runat="server" />
    <input type="hidden" id="hidCodEstado" value="" runat="server" />
    <input type="hidden" id="hidRowDocumento" value="" runat="server" />
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
                    <div class="css_lbl_titulo" style="width: 400px">
                        Detalle del Bien :: Bienes por Contrato
                    </div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" />
                            Volver </a>
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
                        <%-- DATOS DEL CONTRATO--%>
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
                                    <td  runat="server" id="txtNumeroContrato" class="input">
                                        <%--<input id="txtNumeroContrato" name="txtNumeroContrato" runat="server" type="text"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label" style="width: 150px">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input" runat="server" id="txtclasificacion">
                                        <%--input id="txtclasificacion" name="txtclasificacion" runat="server" type="text" style="width: 150px"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label">
                                        Tipo del Bien
                                    </td>
                                    <td class="input" runat="server" id="txtTipoBien">
                                        <%--<input id="txtTipoBien" name="txtTipoBien" type="text" runat="server" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input" runat="server" id="txtMoneda">
                                        <%--<input id="txtMoneda" name="txtmoneda" type="text" runat="server" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <%--<td class="label">
                                        Valor del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtValorBien" name="txtValorBien" runat="server" type="text" style="width: 150px"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />
                                    </td>--%>
                                    <td class="label">
                                        Estado del Contrato
                                    </td>
                                    <td class="input" runat="server" id="txtEstadoContrato">
                                        <%--<input id="txtEstadoContrato" name="txtEstadoContrato" runat="server" type="text"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label">
                                        Banca
                                    </td>
                                    <td class="input" runat="server" id="txtBanca">
                                        <%--<input id="txtBanca" name="txtBanca" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Ejecutivo
                                    </td>
                                    <td class="input" runat="server" id="txtEjecutivo">
                                        <%--<input id="txtEjecutivo" name="txtEjecutivo" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label">
                                        Nro. Kardex
                                    </td>
                                    <td class="input" runat="server" id="txtKardex">
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
                                            style="width: 585px" cols="50" type="text" rows="2" class="css_input"
                                            value="" readonly="readonly" disabled="disabled"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%-- DATOS DEL CLIENTE--%>
                        <br />
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Cliente
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos_Cabecera" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input" runat="server" id="txtCUCliente">
                                        <%--<input id="txtCUCliente" name="txtcuCliente" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                    <td class="label" style="width: 150px">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input" colspan="5" runat="server" id="txtRazonSocial">
                                        <%--<input id="txtRazonSocial" name="txtRazonSocial" runat="server" type="text" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" style="width: 150px" />--%>
                                    </td>
                                    <td class="label">
                                        Tipo de Documento
                                    </td>
                                    <td class="input" runat="server" id="txtTipoDocumento">
                                        <%--<input id="txtTipoDocumento" name="txtTipoDocumento" type="text" runat="server" class="css_input_inactivo"
                                            value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Nro. Documento
                                    </td>
                                    <td class="input" runat="server" id="txtNumeroDocumento">
                                        <%--<input id="txtNumeroDocumento" name="txtNumeroDocumento" type="text" runat="server"
                                            class="css_input_inactivo" value="" readonly="readonly" disabled="disabled" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--
        			    BIENES E INMUEBLES
        			    !-->
                        <input type="button" name="btnCargarInmuebles" id="btnCargarInmuebles" onclick="javascript:fn_buscarInmuebles();"
                            style="display: none;" />
                        <div id="dv_bienesinmuebles" runat="server">
                            <div id="divTabs" style="border: 0; background: none;" class="dv_tabla_contenedora">
                                <ul>
                                    <li><a href="#tab-0">DATOS DEL BIEN</a></li>
                                    <li><a href="#tab-1">REGISTROS PUBLICOS Y MUNICIPALES</a></li>
                                    <li><a href="#tab-2">COMENTARIOS Y ADJUNTOS</a></li>
                                </ul>
                                <div id="tab-0">
                                    <div class="dv_tabla_contenedora" style="border: 0; background: none;">
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td class="label" style="width: 180px">
                                                    Estado del Registro del Bien
                                                </td>
                                                <td>
                                                    <select id="ddlEstadoBienInmueble" runat="server" name="ddlEstadoBienInmueble" onchange="javascript:ValidarEstadosBien(this.value);">
                                                        <option value="0">Todos</option>
                                                    </select>
                                                </td>
                                                <td align="left">
                                                    <br />
                                                    
                                                  
                                                    <div class="dv_img_boton_mini" id="div11" style="border: 0">
                                                        <a href="javascript:fn_EditarInmueble();">
                                                            <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;"
                                                                border="0" />Ver</a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                            <%--<tr>
                                                <td align="left">
                                                    <br />
                                                    <div class="dv_img_boton_mini" id="divAgregar" style="border: 0">
                                                        <a href="javascript:fn_AbreRegistroInmuebles();" style="display: inline;">
                                                            <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                                border="0" />Agregar </a>
                                                    </div>
                                                    <div class="dv_img_boton_mini" style="border: 0" id="divEliminarInmueble" runat="serVer Detalle">
                                                        <a href="javascript:fn_EliminarBien();">
                                                            <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                                border="0" />Eliminar </a>
                                                    </div>
                                                    <div class="dv_img_boton_mini" id="divEditar" style="border: 0">
                                                        <a href="javascript:fn_EditarInmueble();">
                                                            <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                                border="0" />Editar </a>
                                                    </div>
                                                </td>
                                            </tr>--%>
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
                                </div>
                                <!--
        			        DATOS DE REGISTROS PUBLICOS
        			        !-->
                                <div id="tab-1">
                                    <div class="dv_tabla_contenedora">
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
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
                                                <td class="input" runat="server" id="txtFechaFinObra">
                                                    <%--<input id="txtFechaFinObra" name="txtFechaFinObra" runat="server" type="text" />--%>
                                                </td>
                                                <td class="label">
                                                    Fecha Real de Fin de Obra
                                                </td>
                                                <td class="input" runat="server" id="txtFechaRealFinObra">
                                                    <%--<input id="txtFechaRealFinObra" name="txtFechaRealFinObra" runat="server" type="text" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Estado Inscripción Municipal
                                                </td>
                                                <td class="input" id="ddlEstadoInscripcionMunicipal" runat="server">
                                                    <%--<select id="ddlEstadoInscripcionMunicipal" runat="server" name="ddlEstadoInscripcionMunicipal">
                                                        <option value='0'>[-Seleccione-]</option>
                                                    </select>--%>
                                                </td>
                                                <td class="label">
                                                    Fecha Inscripción Municipal
                                                </td>
                                                <td class="input" runat="server" id="txtFechaInscripcionMunicipal">
                                                    <%--<input id="txtFechaInscripcionMunicipal" name="txtFechaInscripcionMunicipal" runat="server"
                                                        type="text" onkeyup="fn_ValidarInscripcionMunicipal();" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Notaría
                                                </td>
                                                <td class="input" runat="server" id="ddlNotaria">
                                                    <%--<select id="ddlNotaria" runat="server" name="ddlNotaria">
                                                        <option value='0'>[-Seleccione-]</option>
                                                    </select>--%>
                                                </td>
                                                <td class="label">
                                                    Fecha de Envío a Notaría
                                                </td>
                                                <td class="input" id="txtFechaEnvioNotaria" runat="server">
                                                    <%--<input id="txtFechaEnvioNotaria" name="txtFechaEnvioNotaria" runat="server" type="text" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Estado Inscripción Registral
                                                </td>
                                                <td class="input" runat="server" id="ddlEstadoInscripcionRRPP">
                                                    <%--<select id="ddlEstadoInscripcionRRPP" runat="server" name="ddlEstadoInscripcionRRPP">
                                                        <option value='0'>[-Seleccione-]</option>
                                                    </select>--%>
                                                </td>
                                                <td class="label">
                                                    Fecha Inscripción Registral
                                                </td>
                                                <td class="input" id="txtFechaInscripcionRegistral" runat="server">
                                                    <%--<input id="txtFechaInscripcionRegistral" name="txtFechaInscripcionRegistral" runat="server"
                                                        type="text" onkeyup="javascript:fn_ValidarInscripcionRegistral();" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Oficina Registral
                                                </td>
                                                <td class="input" runat="server" id="txtOficinaRegistral">
                                                    <%--<input id="txtOficinaRegistral" name="txtOficinaRegistral" runat="server" type="text" />--%>
                                                </td>
                                                <td class="label">
                                                    Fecha de Propiedad
                                                </td>
                                                <td class="input" id="txtFechaPropiedad" runat="server">
                                                    <%--<input id="txtFechaPropiedad" name="txtFechaPropiedad" runat="server" type="text" />--%>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td class="label">
                                                    Propiedad
                                                </td>
                                                <td class="input">
                                                    <select id="ddlPropiedad" runat="server" name="ddlPropiedad">
                                                        <option value='0'>[-Seleccione-]</option>
                                                    </select>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </div>
                                </div>
                                <!--
        			        COMENTARIOS Y ADJUNTOS
        			        !-->
                                <div id="tab-2">
                                <%--InicioJJM--%>
                                    <%--<input type="button" name="btnCargarAdjuntos" id="btnCargarAdjuntos" onclick="javascript:fn_ListagrillaDocumentos();"
                                        style="display: none;" />--%>
                                        <input type="button" name="btnCargarAdjuntos" id="btnCargarAdjuntos" onclick="javascript:fn_ReloadGrillas();"
                                        style="display: none;" />
                                        <%--FinJJM--%>
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
                                            <td colspan="6" valign="top">
                                                <table id="jqGrid_lista_I">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_I">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!--
        			    Maquinarias
        			    !-->
        			    <input type="button" name="btnCargarMaquinaria" id="btnCargarMaquinaria" onclick="javascript:fn_buscarMaquinarias();"
                            style="display: none;" />                       
                        <div id="dv_Maquinarias" runat="server">
                            <div id="divTabsM" style="border: 0; background: none;" class="dv_tabla_contenedora">
                                <ul>
                                    <li><a href="#tab-3">DATOS DEL BIEN</a></li>
                                    <%--Inicio-JJM--%>
                                    <li><a href="#tab-6">COMENTARIOS Y ADJUNTOS</a></li>
                                    <%--FinJJM--%>
                                </ul>
                                <div id="tab-3">
                                    <div class="dv_tabla_contenedora" style="border: 0; background: none;">
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td class="label" style="width: 180px">
                                                    Estado del Registro del Bien
                                                </td>
                                                <td>
                                                    <select id="ddlEstadoBienMaquinariaOtros" runat="server" name="ddlEstadoBienInmueble"
                                                        onchange="javascript:ValidarEstadosMaquinaria(this.value);">
                                                    </select>
                                                </td>
                                                <td colspan="6" align="left">
                                                    <br />
                                                    <div id="dv_botonesMaquinaria" style="border: 0px">
                                                       
                                                        <div class="dv_img_boton_mini" id="div5" style="border: 0">
                                                            <a href="javascript:fn_EditarMaquinarias();">
                                                                <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;"
                                                                    border="0" />Ver</a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                            <%-- <tr>
                                                
                                            </tr>--%>
                                            <tr>
                                                <td colspan="6" valign="top">
                                                    <table id="jqGrid_lista_B">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_B">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <%--Inicio-JJM--%>
                                <!--
        			        COMENTARIOS Y ADJUNTOS
        			        !-->
                                <div id="tab-6">                                   
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <tr>
                                            <td colspan="6" align="left">
                                             
                                                <div class="dv_img_boton_mini" id="div17" style="border: 0">
                                                    <a href="javascript:fn_abreEditarDocumentoComentarioMaquina();">
                                                        <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                            border="0" />Ver Detalle </a>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" valign="top">
                                                <table id="jqGrid_lista_K">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_K">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%--FinJJM--%>
                            </div>
                        </div>
                        <!--
        			    Unidad de Transporte Liviano, Pesado
        			    !--> 
        			    <input type="button" name="btnCargarVehiculo" id="btnCargarVehiculo" onclick="javascript:ListaBienContratoVehiculos();"
                            style="display: none;" />                                  
                        <div id="dv_UnidadTransporte" runat="server">
                            <div id="divTabsV" style="border: 0; background: none;" class="dv_tabla_contenedora">
                                <ul>
                                    <li><a href="#tab-4">DATOS DEL BIEN</a></li>
                                    <%--Inicio-JJM--%>
                                    <li><a href="#tab-7">COMENTARIOS Y ADJUNTOS</a></li>
                                    <%--FinJJM--%>
                                </ul>
                                <div id="tab-4">
                                    <div class="dv_tabla_contenedora" style="border: 0; background: none;">
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td class="label" style="width: 180px">
                                                    Estado del Registro del Bien
                                                </td>
                                                <td>
                                                    <select id="ddlEstadoBienUnidadTransporte" runat="server" name="ddlEstadoBienUnidadTransporte"
                                                        onchange="javascript:ValidarEstadosVehiculos(this.value);">
                                                        <option value='0'>Activo</option>
                                                    </select>
                                                </td>
                                                <td colspan="6" align="left">
                                                    <br />
                                                 
                                                    <div class="dv_img_boton_mini" id="div9" style="border: 0">
                                                        <a href="javascript:fn_EditarVehiculos();">
                                                            <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;"
                                                                border="0" />Ver</a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                            <%--<tr>
                                                
                                            </tr>--%>
                                            <tr>
                                                <td colspan="6" valign="top">
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
                                <%--Inicio-JJM--%>
                                <!--
        			        COMENTARIOS Y ADJUNTOS
        			        !-->
                                <div id="tab-7">                                   
                                    <table id="Table4" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <tr>
                                            <td colspan="6" align="left">
                                               
                                                <div class="dv_img_boton_mini" id="div25" style="border: 0">
                                                    <a href="javascript:fn_abreEditarDocumentoComentarioTransporte();">
                                                        <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                            border="0" />Ver Detalle </a>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" valign="top">
                                                <table id="jqGrid_lista_J">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                                <div id="jqGrid_pager_J">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%--FinJJM--%>
                            </div>
                        </div>
                        <!--    sistemas, otros
        			    !-->
                        <input type="button" name="btnCargarDatosOtros" id="btnCargarDatosOtros" onclick="javascript:fn_buscarSistemasOtros();"
                            style="display: none;" />
                        <div id="dv_SistemasOTros" runat="server">
                            <div id="divTabsS" style="border: 0; background: none;" class="dv_tabla_contenedora">
                                <ul>
                                    <li><a href="#tab-5">DATOS DEL BIEN</a></li>
                                    <%--Inicio-JJM--%>
                                    <li><a href="#tab-8">COMENTARIOS Y ADJUNTOS</a></li>
                                    <%--FinJJM--%>
                                </ul>
                                <div id="tab-5">
                                    <div class="dv_tabla_contenedora" style="border: 0; background: none;">
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td class="label" style="width: 180px">
                                                    Estado del Registro del Bien
                                                </td>
                                                <td>
                                                    <select id="ddlEstadoBienSistemas" runat="server" name="ddlEstadoBienInmueble" onchange="javascript:ValidarEstadosSistemas(this.value);">
                                                    </select>
                                                </td>
                                                <td colspan="6" align="left">
                                                    <div id="Div12" style="border: 0px">
                                                    
                                                        <div class="dv_img_boton_mini" id="div14" style="border: 0">
                                                            <a href="javascript:fn_EditarSistemasOtros();">
                                                                <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;"
                                                                    border="0" />Ver</a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                            <%--<tr>
                                                
                                            </tr>--%>
                                            <tr>
                                                <td colspan="6" valign="top">
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
                                </div>
                                <%--Inicio-JJM--%>
                                <!--
        			        COMENTARIOS Y ADJUNTOS
        			        !-->
                                <div id="tab-8">
                                    <input type="button" name="btnCargarAdjuntos4" id="btnCargarAdjuntos4" onclick="javascript:fn_ListagrillaDocumentosSistema();"
                                        style="display: none;" />
                                    <table id="Table1" border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                                        <tr>
                                            <td colspan="6" align="left">
                                               
                                                <div class="dv_img_boton_mini" id="div19" style="border: 0">
                                                    <a href="javascript:fn_abreEditarDocumentoComentarioSistema();">
                                                        <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                            border="0" />Ver Detalle </a>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" valign="top">
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
                                <%--FinJJM--%>
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
