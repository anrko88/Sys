<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProveedorMant.aspx.vb"
    Inherits="Mantenimiento_frmProveedorMant" %>

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

    <script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"> </script>

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

    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>

    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>

    <script src="../Util/js/jquery/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>

    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmProveedorMant.aspx.js"> </script>

</head>
<body>
    <form id="frmProveedorMant" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Mantenimiento</div>
                    <div class="css_lbl_titulo">
                        Proveedor : Nuevo</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" title="Cancelar" /><br />
                            Volver </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_grabar();">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" title="Guardar" /><br />
                            Guardar </a>
                    </div> 
                    <div id="dv_img_boton3" class="dv_img_boton">
                        <a href="javascript:fn_eliminar();">
                            <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" title="Guardar" /><br />
                            Eliminar </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Proveedor
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <%--     <tr>
                                    <td style="width: 150px">
                                    </td>
                                    <td style="width: 250px">
                                    </td>
                                    <td style="width: 150px">
                                    </td>
                                    <td style="width: 250px">
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="label">
                                        Procedencia
                                    </td>
                                    <td class="input">
                                        <select id="ddlProcedencia" runat="server" onchange="fn_oculta_controles(this.value)">
                                            <option value="0">- Seleccionar -</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Tipo de Persona
                                    </td>
                                    <td class="input">
                                        <select name="cmbTipoPersona" id="cmbTipoPersona" runat="server" onchange="javascript:fn_ValidarCampo_TipoPersona(this.value);">
                                            <option>- Seleccionar -</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo de Documento
                                    </td>
                                    <td class="input">
                                        <select id="cmbTipoDocumento" name="cmbTipoDocumento" runat="server" onchange="javascript:fn_validarCampos(this.value);">
                                            <option value="">- Seleccionar -</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Nro. Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocumento" runat="server" style="text-align: right" type="text"
                                            class="css_input" size="15" value="" />
                                        <img id="imgBsqClienteRM" alt="" src="../Util/images/ico_buscar.jpg" style="cursor:pointer; vertical-align:middle;" runat="server" />
                                        <input type="hidden" id="HidCodigoUnico" value="0" runat="server" />
                                        <input type="hidden" id="HidPerfil" value="" runat="server" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" runat="server" type="text" class="css_input" style="width: 450px"
                                            value="" />
                                    </td>
                                    <td class="label"  id="tdlDireccion">
                                        Dirección
                                    </td>
                                    <td class="input"  id="tdcDireccion">
                                        <input id="txtDireccion" type="text" runat="server" class="css_input" style="width: 450px"
                                            value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        País
                                    </td>
                                    <td class="input">
                                        <select id="ddlPais" class="" runat="server">
                                            <option value="0">- Seleccionar -</option>
                                        </select>
                                    </td>
                                    <td class="label" id="tdlDepartamento">
                                        Departamento
                                    </td>
                                    <td class="input" id="tdcDepartamento">
                                        <select id="ddlDepartamento" name="ddlDepartamento" runat="server" onchange="javascript:fn_cargaComboProvincia(this.value);">
                                            <option value="0">- Seleccionar -</option>
                                        </select>
                                        <input type="hidden" id="hidCodDepartamento" runat="server" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" id="tdlProvincia" style="display: none">
                                        Provincia
                                    </td>
                                    <td class="input" id="tdcProvincia" style="display: none">
                                        <select id="cmbProvincia" name="cmbProvincia" onchange="javascript:fn_cargaComboDistrito(ddlDepartamento.value,this.value);">
                                            <option value="0">- Seleccionar -</option>
                                        </select>
                                        <input type="hidden" id="hidCodProvincia" runat="server" value="" />
                                    </td>
                                    <td class="label" id="tdlDistrito" style="display: none" >
                                        Distrito
                                    </td>
                                    <td class="input" id="tdcDistrito" style="display: none">
                                        <select id="cmbDistrito" name="cmbProvincia">
                                            <option value="0">- Seleccionar -</option>
                                        </select>
                                        <input type="hidden" id="hidCodDistrito" runat="server" value="" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div id="divCuentas">
                            <table id="tb_tabla_IBK" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="titulo css_lbl_tituloContenido">
                                        Datos de Cuentas Interbank
                                    </td>
                                </tr>
                            </table>
                            <div id="dv_datosIBK" class="dv_tabla_contenedora">
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td class="label">
                                            Tipo de Cuenta 1
                                        </td>
                                        <td class="input">
                                            <select id="cmbTipoCuenta1" class="" runat="server">
                                                <option value="0" selected="selected">- Seleccionar -</option>
                                            </select>
                                            <input type="hidden" id="hidCodProveedorCuenta1" runat="server" value="0" />
                                             <input type="hidden" id="hddValidaCuenta" runat="server" />
                                            
                                        </td>
                                        <td class="label">
                                            Moneda
                                        </td>
                                        <td class="input">
                                            <select id="cmbMoneda1" runat="server">
                                                <option selected="selected">- Seleccionar - </option>
                                            </select>
                                        </td>
                                        <td class="label">
                                            Nro. Cuenta
                                        </td>
                                        <td class="input">
                                            <input id="txtNumCuenta1" maxlength="15" name="txtNumCuenta1" runat="server" type="text"
                                                class="css_input" size="40" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Tipo de Cuenta 2
                                        </td>
                                        <td class="input">
                                            <select id="cmbTipoCuenta2" class="" runat="server">
                                                <option value="0" selected="selected">- Seleccionar -</option>
                                            </select>
                                            <input type="hidden" id="hidCodProveedorCuenta2" runat="server" value="0" />
                                        </td>
                                        <td class="label">
                                            Moneda
                                        </td>
                                        <td class="input">
                                            <select id="cmbMoneda2" runat="server">
                                                <option selected="selected">- Seleccionar - </option>
                                            </select>
                                        </td>
                                        <td class="label">
                                            Nro. Cuenta
                                        </td>
                                        <td class="input">
                                            <input id="txtNumCuenta2" maxlength="15" name="txtNumCuenta2" runat="server" type="text"
                                                class="css_input" size="40" value="" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table id="tb_tabla_BN" border="0" cellpadding="0" cellspacing="3" runat="server">
                                <tr>
                                    <td class="titulo css_lbl_tituloContenido">
                                        Datos de Cuentas Banco de la Nación (Detracción)
                                    </td>
                                </tr>
                            </table>
                            <div id="dv_datosBN" class="dv_tabla_contenedora" runat="server">
                                 
                                      <div style="display: none">
                                         <select id="cmbTipoCuenta3" class="" runat="server">
                                                <option value="0" selected="selected">- Seleccionar -</option>
                                            </select>
                                            <input type="hidden" id="hidCodProveedorCuenta3" runat="server" value="0" /> 
                                      </div>
                                  
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                    <tr>
                                     <td class="label" >
                                            Moneda
                                        </td>
                                        <td class="input" >
                                            <select id="cmbMoneda3" runat="server" style="width: 200px">
                                                <option selected="selected">- Seleccionar - </option>
                                            </select>
                                        </td>
                                        <td class="label" >
                                            Nro. Cuenta
                                        </td>
                                        <td class="input" >
                                            <input id="txtNumCuenta3" maxlength="15" name="txtNumCuenta3" runat="server" type="text"
                                                class="css_input" size="40" value="" style="width: 200px"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <table id="tb_tabla_cliente" border="0" cellpadding="0" cellspacing="3">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contacto
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datosCliente" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <%--  <tr>
                                    <td style="width: 150px">
                                    </td>
                                    <td style="width: 250px">
                                    </td>
                                    <td style="width: 150px">
                                    </td>
                                    <td style="width: 250px">
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="label" >
                                        Nombre del Contacto
                                    </td>
                                    <td class="input">
                                        <input id="txtNombreContacto" maxlength="200" type="text" class="css_input" style="width: 200px"
                                            value="" />
                                        <input type="hidden" id="hidCodigoContacto" value="0" />
                                        <input type="hidden" id="hidEliminarContacto" value="" />
                                        <input type="hidden" id="hidCodProveedor" runat="server" value="0" />
                                        <input type="hidden" id="hidOpcion" value="0" runat="server" />
                                    </td>
                                    <td class="label" >
                                        Correo
                                    </td>
                                    <td class="input">
                                        <input id="txtCorreo" maxlength="120" type="text" class="css_input" value="" style="width: 200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" >
                                        Teléfono
                                    </td>
                                    <td class="input">
                                        <input id="txtTelefono" maxlength="15" type="text" class="css_input" value="" style="width: 200px" />
                                    </td>
                                    <td class="label">
                                        Cargo
                                    </td>
                                    <td class="input">
                                        <select id="ddlCargo" runat="server">
                                            <option></option>
                                        </select>
                                    </td>
                                </tr>
                                </table>
                                <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                   <tr>
                                    <td colspan="4" align="right">
                                        <div id="dv_BotonesEdicion" style="border: 0; display: none">
                                            <div id="Div1" class="dv_img_boton_mini" style="height: 30px; border: 0;">
                                                <a href="javascript:fn_editarContacto();" style="display: inline;">
                                                    <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;
                                                        border: 0;" />Guardar</a>
                                            </div>
                                            <div id="Div2" class="dv_img_boton_mini" style="height: 30px; border: 0;">
                                                <a href="javascript:fn_CancelarContacto();" style="display: inline;">
                                                    <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                        border: 0;" />Cancelar</a>
                                            </div>
                                        </div>
                                        <div id="dv_BotonesNormales" style="border: 0; display: block">
                                            <div class="dv_img_boton_mini" id="divCancelar" style="border: 0">
                                                <a href="javascript:fn_cancelarContacto();" style="display: inline;">
                                                    <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                        display: inline;" border="0" />Cancelar </a>
                                            </div>
                                            <div class="dv_img_boton_mini" id="divAgregar" style="border: 0">
                                                <a href="javascript:fn_agregarContacto();" style="display: inline;">
                                                    <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                        display: inline;" border="0" />Agregar </a>
                                            </div>
                                            <div class="dv_img_boton_mini" style="border: 0">
                                                <a href="javascript:fn_eliminarContacto();">
                                                    <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />Eliminar </a>
                                            </div>
                                            <div class="dv_img_boton_mini" id="divEditar" style="border: 0">
                                                <a href="javascript:fn_EditarContacto();">
                                                    <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;"
                                                        border="0" />&nbsp;&nbsp;&nbsp; Editar </a>
                                            </div>
                                        </div>
                                        <br />
                                        <%--<div class="dv_tabla_contenedora">--%>
                                            <input id="hddRowId" type="hidden" runat="server" />
                                            <table id="jqGrid_lista_B">
                                                <tr>
                                                    <td />
                                                </tr>
                                            </table>
                                            <div id="jqGrid_pager_B">
                                            </div>
                                        <%--</div>--%>
                                    </td>
                                </tr> 
                                    
                                </table>
                                
                            
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
