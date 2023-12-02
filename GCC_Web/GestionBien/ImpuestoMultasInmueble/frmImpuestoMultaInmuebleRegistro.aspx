<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImpuestoMultaInmuebleRegistro.aspx.vb"
    Inherits="GestionBien_ImpuestoMultasInmueble_frmImpuestoMultaInmuebleRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
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

    <script type='text/javascript' src="frmImpuestoMultaInmuebleRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmImpuestoMultaInmuebleRegistro" runat="server">
    <input type="hidden" name="hddTipoTx" id="hddTipoTx" value="" runat="server" />
    <input type="hidden" name="hddVer" id="hddVer" value="" runat="server" />
    <input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
    <input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />
    <input type="hidden" name="hddCodUnico" id="hddCodUnico" value="" runat="server" />
    <input type="hidden" name="hddCodImpuesto" id="hddCodImpuesto" value="" runat="server" />
    <input type="hidden" name="hddAbreEditarAuto" id="hddAbreEditarAuto" value="" runat="server" />
    <input type="hidden" name="hddCodigosImpuestos" id="hddCodigosImpuestos" value=""
        runat="server" />
    <input type="hidden" name="hddRowIdImpuesto" id="hddRowIdImpuesto" value="" runat="server" />
    <input type="hidden" name="hddFechaTransferencia" id="hddFechaTransferencia" value=""
        runat="server" />
    <input type="button" name="btnBuscarImpuestos" id="btnBuscarImpuestos" onclick="javascript:fn_buscarImpuestos();"
        style="display: none;" />
    <input type="hidden" name="hddEstadoPago" id="hddEstadoPago" value="" runat="server" />
    <input type="hidden" name="hddPeriodoEdita" id="hddPeriodoEdita" value="" runat="server" /><%--JJM IBK--%>
    <input type="hidden" name="hddNroLote" id="hddNroLote" value="" runat="server" /><%--JJM IBK--%>
    <input type="hidden" name="hddMunicipalidad" id="hddMunicipalidad" value="" runat="server" /><%--JJM IBK--%>
    <input type="hidden" name="hddMunicipalidadBien" id="hddMunicipalidadBien" value=""
        runat="server" /><%--JJM IBK--%>
    <input type="hidden" name="hddCodMunicipalidad" id="hddCodMunicipalidad" value="" runat="server" /><%--JJM IBK--%>        
    <input type="hidden" name="hddTotalPredial" id="hddTotalPredial" value="" runat="server" /><%--JJM IBK--%>
    <input type="hidden" name="hddTotalAutovaluo" id="hddTotalAutovaluo" value="" runat="server" /><%--JJM IBK--%>
    <input type="hidden" name="hddPeriodo" id="hddPeriodo" value="" runat="server" /><%--JJM IBK--%>
    <input type="hidden" name="hddPerfil" id="hddPerfil" value="" runat="server" /><%--JJM IBK--%>
    <input type="button" name="btnBuscarImpuestosxLote" id="btnBuscarImpuestosxLote"
        onclick="javascript:fn_buscarImpuestosLote();" style="display: none;" /><%--JJM IBK--%>
   <%--Inicio IBK - AAE - Variable para ver si tengo el nro del lote asignado --%>
    <input id="hidTengoLote" type="hidden" runat="server" />
    <%--Fin IBK - AAE--%>      
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_impuesto.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Impuestos y Multas Inmuebles :: Registro</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_botonCancelar" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_btnBuscar" class="dv_img_boton">
                        <a href="javascript:fn_listaBusquedaBien();">
                            <img alt="" src="../../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_btnGeneraLote" class="dv_img_boton" style="width: 80px;">
                        <a href="javascript:fn_generaLote();">
                            <img alt="" src="../../Util/images/ico_acc_lote.gif" border="0" /><br />
                            Genera Lote </a>
                    </div>
                    <div id="dv_img_anul" class="dv_img_boton" style="width: 80px;">
                        <a href="javascript:fn_AnularLote()">
                            <img alt="" src="../../Util/images/ico_acc_eliminar.gif" border="0" /><br />
                            Anular Lote </a>
                    </div>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="NUEVO"
            runat="server" />
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <div id="dv_busquedaBienes">
                            <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="titulo css_lbl_tituloContenido">
                                        Búsqueda de bienes
                                    </td>
                                    <td class="botones">
                                        <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                    </td>
                                </tr>
                            </table>
                            <div class="dv_tabla_contenedora">
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <%--Inicio IBK - AAE - Variable para ver si tengo el nro del lote asignado --%>
                                <tr>
                                    <td class="label">
                                        Nro Lote
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtNroLoteCarga" class="css_input" runat="server" />                                    
                                    </td>
                                    <td class="label">
                                        Estado
                                    </td>
                                    <td class="input">
                                        <input id="hidCodEstadoLote" type="hidden" runat="server" />
                                        <input type="text" id="txtEstadoLote" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Total Lote
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtTotalLote" class="css_input" runat="server" />
                                    </td>                                
                                </tr>
                                <tr>
                                <td class="label">
                                        Monto Cheque
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtMontoCheque" class="css_input" runat="server" />
                                    </td>                                   
                                    <td class="label">
                                        Devuelto
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtDevuelto" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        A Reembolsar
                                    </td>
                                    <td class="input">
                                        <input type="text" id="txtReembolsar" class="css_input" runat="server" />
                                    </td>
                                </tr> 
                                <%--Fin IBK - AAE--%>                               
                                    <tr>
                                        <td class="label">
                                            Departamento
                                        </td>
                                        <td class="input">
                                            <select id="cmbDepartamento" name="cmbDepartamento" onchange="javascript:fn_cargaComboProvincia(this.value);"
                                                runat="server">
                                                <option value="0">[-Seleccionar-]</option>
                                            </select>
                                        </td>
                                        <td class="label">
                                            Provincia
                                        </td>
                                        <td class="input">
                                            <select id="cmbProvincia" name="cmbProvincia" onchange="javascript:fn_cargaComboDistrito(cmbDepartamento.value,this.value);"
                                                runat="server">
                                                <option value="0">[-Seleccionar-]</option>
                                            </select>
                                        </td>
                                        <td class="label">
                                            Distrito
                                        </td>
                                        <td class="input">
                                            <select id="cmbDistrito" name="cmbDistrito" runat="server">
                                                <option value="0">[-Seleccionar-]</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--Inicio JJM IBK--%>
                                        <td class="label">
                                            <%--Nro Lote--%>
                                        </td>
                                        <td class="input">
                                            <%--<input type="text" id="txtNroLoteCarga_old" class="css_input" runat="server" />
                                            <input id="hidCodEstadoLote_old" type="hidden" runat="server" />
                                            <input type="text" id="txtEstadoLote_old" class="css_input" runat="server" />--%>
                                        </td>
                                        <td class="label">
                                            Municipalidad
                                        </td>
                                        <td class="input">
                                            <input id="txtCodMunicipalidad" runat="server" class="css_input" size="5" />
                                            <input id="txtMunicipalidadDesc" runat="server" class="css_input" size="40" />
                                            <img id="imgBsqMunicipalidad" alt="" src="../../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                                vertical-align: top;" runat="server" />
                                            <%--<select id="cmbMunicipalidad" name="cmbMunicipalidad" runat="server">
                                                <option value="0">[-Seleccionar-]</option>
                                            </select>--%>
                                        </td>
                                        <%--Fin JJM IBK--%>
                                        <td class="label">
                                            Dirección
                                        </td>
                                        <td class="input" colspan="5">
                                            <input id="txtUbicacion" type="text" class="css_input" runat="server" size="50" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                        </div>
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Listado de Bienes
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora">
                            <div id="dv_AgregarImpuesto">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" id="tb_formulario">
                                    <tr>
                                        <%--<td class="label">
											Periodo
										</td>
										<td class="input" style="width:150px">
											<input id="txtPeriodo" type="text" class="css_input" runat="server" size="12" />
										</td>
										<td class="label">
											Total Autovaluo
										</td>
										<td class="input" style="width:150px">
											<input id="txtTotalAutovaluo" type="text" class="css_input" runat="server" size="12" />
										</td>                                
										<td class="label">
											Total Predial 
										</td>
										<td class="input" style="width:150px">
											<input id="txtTotalPredial" type="text" class="css_input" runat="server" size="12" />
										</td>  
										<td colspan="6" style="text-align: right; padding-right: 5px">                              
											<div id="dv_agregar" class="dv_img_boton_mini" style="width: 120px; border: 0px solid #ffffff;" dir="ltr">
												<a href="javascript:fn_agregarImpuesto();">
													<img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;" border="0" />
													Agregar Impuesto
												</a>
											</div>                                            
										</td>--%>
                                    </tr>
                                </table>
                            </div>
                            <table id="jqGrid_lista_A">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_A">
                            </div>
                        </div>
                        <br />
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Listado de Impuestos y Multas
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <%--<td class="label" visible="false">
                                        
                                    </td>
                                    <td class="input" style="width: 150px">
                                        <input id="txtMunicipalidad" type="text" class="css_input_inactivo" runat="server"
                                            size="12" visible="false" />
                                    </td>--%>
                                    <td class="label">
                                        Periodo
                                    </td>
                                    <td class="input" style="width: 150px">
                                        <input id="txtPeriodo" type="text" class="css_input" runat="server" size="12" />
                                    </td>
                                    <td class="label">
                                        Total Autovaluo
                                    </td>
                                    <td class="input" style="width: 150px">
                                        <input id="txtTotalAutovaluo" type="text" class="css_input" runat="server" size="12" />
                                    </td>
                                    <td class="label">
                                        Total Predial
                                    </td>
                                    <td class="input" style="width: 150px">
                                        <input id="txtTotalPredial" type="text" class="css_input" runat="server" size="12" />
                                    </td>
                                    <td class="label">
                                        Dscto.
                                    </td>
                                    <td class="input">
                                        <input id="txtDscto" type="text" class="css_input" runat="server" size="2" />
                                        <%--<img id="img1" alt="" src="../../Util/images/ico_buscar.jpg" style="cursor: pointer;
                                                vertical-align: top;" runat="server" />--%>
                                        <%--Escoger uno de los 2--%>
                                    </td>
                                    <td>
                                        <a href="javascript:fn_EjecutaDescuento();">
                                            <img alt="" src="../../Util/images/ico_mdl_tasacion.gif" style="width: 16px; height: 16px;"
                                                border="0" />
                                        </a>
                                    </td>
                                    <td colspan="6" style="text-align: right; padding-right: 5px">
                                        <div id="dv_agregar" class="dv_img_boton_mini" style="width: 120px; border: 0px solid #ffffff;"
                                            dir="ltr">
                                            <a href="javascript:fn_agregarImpuesto();">
                                                <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Agregar Impuesto </a>
                                        </div>
                                    </td>
                                    <td colspan="5" style="text-align: right; padding-right: 5px">
                                        <div id="dv_eliminar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
                                            runat="server">
                                            <a href="javascript:fn_eliminarImpuesto();">
                                                <img alt="" src="../../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Eliminar </a>
                                        </div>
                                        <div id="dv_editar" class="dv_img_boton_mini" style="width: 60px; border: 0px solid #ffffff;"
                                            runat="server">
                                            <a href="javascript:fn_editarImpuesto();">
                                                <img alt="" src="../../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Editar </a>
                                        </div>
                                        <div id="dv_ver" class="dv_img_boton_mini" style="width: 100px; border: 0px solid #ffffff;"
                                            runat="server">
                                            <a href="javascript:fn_verImpuesto();">
                                                <img alt="" src="../../Util/images/ico_mdl_cotizacion.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Ver Impuesto</a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table id="jqGrid_lista_B">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_B">
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!-- Fin Cuerpo -->
    </div>
    </form>
</body>
</html>
