<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTasacionAsignacionListado.aspx.vb"
    Inherits="GestionBien_Tasacion_frmTasacionAsignacionListado" %>

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

    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmTasacionAsignacionListado.aspx.js"> </script>

</head>
<body>
    <form id="frmTasacionAsignacionListado" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien :: Tasación</div>
                    <div class="css_lbl_titulo">
                        Asignación :: Listado</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscar(true);">
                            <img alt="" src="../../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_limpiar();">
                            <img src="../../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_AsignarActivo" class="dv_img_boton">
                        <a href="javascript:fn_asignar();">
                            <img alt="" src="../../Util/images/ico_aprobar_act.gif" border="0" style="height: 33px;
                                width: 34px" /><br />
                            Asignar </a>
                    </div>
                    <div id="dv_AsignarDesactivo" class="dv_img_boton">
                        <a href="javascript:fn_asignar2();">
                            <img alt="" src="../../Util/images/ico_aprobar_inact.gif" border="0" style="height: 34px;
                                width: 34px" /><br />
                            Asignar </a>
                    </div>
                    <div id="dv_individual" class="dv_img_boton">
                        <a href="javascript:fn_asignarindividual();">
                            <img alt="" src="../../Util/images/ico_acc_agregar.gif" border="0" /><br />
                            Individual</a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos de Búsqueda
                                </td>
                                <td class="botones">
                                    <img src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtcontrato" name="txtcontrato" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtcucliente" name="txtcucliente" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label" style="width: 180px">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtrazonsocial" name="txtrazonsocial" type="text" class="css_input" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo Documento
                                    </td>
                                    <td class="input">
                                        <select id="cmbTipoDocumento" name="cmbTipoDocumento" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Nº de Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtnumerodocumento" name="txtnumerodocumento" type="text" class="css_input"
                                            runat="server" />
                                    </td>
                                    <td class="label" style="width: 180px">
                                        Estado Tasación del contrato
                                    </td>
                                    <td class="input">
                                        <select id="cbmEstadoTasacionContrato" name="cbmEstadoTasacionContrato" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input">
                                        <select id="cmbClasificacionBien" name="cmbClasificacionBien" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Banca
                                    </td>
                                    <td class="input">
                                        <select id="cmbbanca" name="cmbbanca" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                    <td class="label" style="width: 180px">
                                        Ejecutivo de Banca
                                    </td>
                                    <td class="input">
                                        <input id="TxtEjecutivoBanca" name="TxtEjecutivoBanca" type="text" class="css_input"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Periodo
                                    </td>
                                    <td class="input">
                                        <input id="txtPeriodo" name="txtPeriodo" type="text" size="10" class="css_input"
                                            runat="server" />
                                    </td>
                                    <td class="label">
                                        Fecha Desde
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaDesde" name="txtFechaDesde" type="text" size="10" class="css_input"
                                            runat="server" />
                                    </td>
                                    <td class="label" style="width: 180px">
                                        Fecha Hasta
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaHasta" name="txtFechaHasta" type="text" size="10" class="css_input"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Estado Contrato
                                    </td>
                                    <td class="input">
                                        <select id="cmbEstadocontrato" name="cmbEstadocontrato" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <!-- Inicio Grilla Listado -->
                        <div class="dv_tabla_contenedoraSola">
                            <table id="jqGrid_lista_A">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_A">
                            </div>
                        </div>
                        <!-- Fin Grilla -->
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
    <!-- Fin Cuerpo -->
    <input type="hidden" id="hddPagChecked" name="hddPagChecked" value="" runat="server" />
    <input type="hidden" id="hddEbanca" name="hddEbanca" value="" runat="server" />
    <input type="hidden" id="hddTasador" name="hddTasador" value="" runat="server" />
    <input type="button" name="cmdListarDocumentos" id="cmdListarDocumentos" onclick="javascript:fn_ListadoAsignacionTasador();"
        style="display: none;" />
    <input type="hidden" id="hddperiodo" name="hddperiodo" value="" runat="server" />
    <input type="hidden" id="hddfdesde" name="hddfdesde" value="" runat="server" />
    <input type="hidden" id="hddfhasta" name="hddfhasta" value="" runat="server" />
    <input type="hidden" id="hddPrimerEB" name="hddPrimerEB" value="" runat="server" />
    <input type="hidden" id="hddRowId" name="hddRowId" value="" runat="server" />
    </form>
</body>
</html>
