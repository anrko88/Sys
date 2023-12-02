<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCobroListado.aspx.vb"
    Inherits="GestionBien_OtrosConceptos_frmCobroListado" %>

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

    <script type='text/javascript' src="frmCobroListado.aspx.js"> </script>

</head>
<body>
    <form id="frmCobroListado" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_version.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo" style="width: 400px">
                        Otros Conceptos :: Consulta de Cobros</div>
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
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreEditar('')">
                            <img alt="" src="../../Util/images/ico_acc_editar.gif" border="0" /><br />
                            Editar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreNuevo();">
                            <img alt="" src="../../Util/images/ico_acc_detalle.gif" border="0" /><br />
                            Registro </a>
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
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%;" id="tb_formulario">
                                <tr>
                                    <td class="label">
                                        N° Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" name="txtNroContrato" type="text" class="css_input" size="10"
                                            runat="server" />
                                    </td>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtCUCliente" name="txtCUCliente" type="text" class="css_input" size="10"
                                            runat="server" />
                                    </td>
                                    <td class="label" style="width:140px">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" name="txtRazonSocial" type="text" class="css_input" size="50" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Nº Lote
                                    </td>
                                    <td class="input">
                                    <input id="txtNroLote" name="txtNroLote" type="text" class="css_input" size="18"
                                            runat="server" />
                                       
                                    </td>
                                    <td class="label">
                                        Estado Cobro
                                    </td>
                                    <td class="input">
                                        <select id="cmbEstadoCobro" name="cmbEstadoCobro" runat="server">
                                        </select>
                                    </td>                                    
                                    <td class="label">
                                        Concepto
                                    </td>
                                    <td class="input" >
                                         <select id="cmbConcepto" name="cmbConcepto" runat="server">
                                        </select>
                                    </td>

                                </tr>
                            </table>
                            <%--<table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        N° Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input" name="txtNroContrato" />
                                    </td>
                                    <td class="label" style="width:140px">
                                        Razón Social o Nombre</td>
                                    <td class="input">
                                        <input id="txtRazonSocial" type="text" class="css_input" size="55" name="txtRazonSocial" /></td>
                                    <td class="label" >
                                        Tipo Documento</td>
                                    <td class="input">
                                        <select id="cmbTipoDocumento" runat="server" name="cmbTipoDocumento">
                                        </select></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        N° Documento</td>
                                    <td class="input">
                                        <input id="txtNroDocumento" type="text" class="css_input" name="txtNroDocumento" /></td>
                                    <td class="label">
                                        Concepto</td>
                                    <td class="input">
                                        <select id="cmbConcepto" runat="server" name="cmbConcepto">
                                        </select></td>
                                    <td class="label">
                                        N° Lote</td>
                                    <td class="input">
                                        <input id="txtNroLote" type="text" class="css_input" name="txtNroLote" /></td>
                                </tr>
                            </table>--%>
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
            <asp:HiddenField ID="hidCodSolicitudCredito" runat="server" />
            <asp:HiddenField ID="hidTipoRubroFinanciamiento" runat="server" />
            <asp:HiddenField ID="hidCodIfi" runat="server" />
            <asp:HiddenField ID="hidTipoRecuperacion" runat="server" />
            <asp:HiddenField ID="hidNumSecRecuperacion" runat="server" />
            <asp:HiddenField ID="hidNumSecRecupComi" runat="server" />
            <asp:HiddenField ID="hidCodComisionTipo" runat="server" />
            <asp:HiddenField ID="hidEstadoRecuperacion" runat="server" />
        </div>
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
