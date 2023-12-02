<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCheckListObservacion.aspx.vb"
    Inherits="Comun_frmCheckListObservacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Observación</title>
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
    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->
    <script type='text/javascript' src="frmCheckListObservacion.aspx.js"> </script>

</head>
<body>
    <form id="frmCheckListObservacion" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <img src="../Util/images/ico_mdl_multa.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                    </div>
                    <div class="css_lbl_titulo">
                        Observaciones</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="Div3" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_botonGuardar" class="dv_img_boton">
                        <a href="javascript:fn_guardar();">
                            <img src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
                    </div>
                     <div id="dv_img_botonGuardar2" class="dv_img_boton">
                        <a href="javascript:fn_guardar2();">
                            <img src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Guardar </a>
                    </div>
                </td>
            </tr>
        </table>
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            padding-right: 0px;">
            <tr>
                <td class="lineas">
                </td>
            </tr>
        </table>
        <br>
        <div id="dvtipoObservaciones">
            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="label">
                        Tipo de Observación
                    </td>
                    <td class="input">
                        <select id="cmbTipoObservacion" name="cmbTipoObservacion" runat="server">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <table id="tb_formulario" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="label">
                    Observación
                </td>
                <td class="input">
                    <textarea id="TxaObs" name="TxaObs" rows="4" cols="20" style="width: 500px" runat="server"></textarea>
                </td>
                
            </tr>
            </table>
            <br/>
            <table id="tb_formulario">
            <tr>
                <td class="label">
                    Historial de Observaciones
                </td>
                <td class="input">
                    
                    <div class="css_scrollPane" style="height: 100px;width: 500px;background-color: lightgray" >
                        
                    <div id="dv_historial" runat="server" style="background-color: lightgray" >
                        
                    </div>
                    </div>
                    
                </td>
               
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnTitulo" runat="server" />
    <asp:HiddenField ID="hflagtipoObs" runat="server" />
    <asp:HiddenField ID="hddCodConDoc" runat="server" />
    <asp:HiddenField ID="hddCodContrato" runat="server" />
    <asp:HiddenField ID="hddAdd" runat="server" />
    </form>
</body>
</html>
